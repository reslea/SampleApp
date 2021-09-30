using EsSample.Orders.Models;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsSample.Orders.Database.Entities
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public int ProductCount { get; set; }

        public bool IsPrepared { get; set; }

        public bool IsPaid { get; set; }

        public void Update(RecordedEvent evt)
        {
            var data = evt.Data;

            var dataJson = Encoding.UTF8.GetString(data);

            switch (evt.EventType)
            {
                case "OrderPaymentFailure":
                    IsPaid = false;
                    break;
                case "OrderPaymentSuccess":
                    var order = JsonConvert.DeserializeObject<OrderModel>(dataJson);

                    ProductCount = order.Products.Count;

                    IsPaid = true;
                    IsPrepared = false;
                    break;
                case "OrderPrepared":
                    IsPrepared = true;
                    break;
                default: break;
            }
        }
    }
}
