using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EsSample.Orders.Database;
using EventStore.ClientAPI;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EsSample.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersDbContext _context;
        private readonly IEventStoreConnection _esConnection;

        public OrdersController(
            OrdersDbContext context,
            IEventStoreConnection esConnection)
        {
            _context = context;
            _esConnection = esConnection;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            return Ok(_context.Orders);
        }

        [HttpPost("ready/{orderId:guid}")]
        public async Task<IActionResult> MarkAsPrepared(Guid orderId)
        {
            var orderCheckpoint = await _context
                .OrderCheckpoints
                .Include(oc => oc.Order)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (orderCheckpoint is null)
            {
                return NotFound();
            }

            await AppendOrderPreparedEvent(orderCheckpoint);

            return Ok();
        }

        private async Task AppendOrderPreparedEvent(OrderCheckpoint orderCheckpoint)
        {
            var jsonStr = JsonConvert.SerializeObject(new
            {
                isPrepared = true
            });
            var jsonBytes = Encoding.UTF8.GetBytes(jsonStr);

            await _esConnection.AppendToStreamAsync(
                $"order-{orderCheckpoint.OrderId}",
                orderCheckpoint.LastProcessedEventNumber,
                new EventData(
                    Guid.NewGuid(),
                    "OrderPrepared",
                    true,
                    jsonBytes,
                    null));
        }
    }
}
