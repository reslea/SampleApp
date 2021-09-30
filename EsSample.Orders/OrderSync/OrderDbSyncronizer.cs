using EsSample.Orders.Database;
using EsSample.Orders.Database.Entities;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EsSample.Orders.OrderSync
{
    public class OrderDbSyncronizer : IOrderDbSyncronizer
    {
        private readonly string streamName = "$ce-order";
        private readonly IEventStoreConnection eventStoreConnection;
        private readonly UserCredentials userCredentials;
        private readonly OrdersDbContext context;

        public OrderDbSyncronizer(
            IEventStoreConnection eventStoreConnection,
            UserCredentials userCredentials,
            OrdersDbContext context)
        {
            this.eventStoreConnection = eventStoreConnection;
            this.userCredentials = userCredentials;
            this.context = context;
        }

        public void ProcessExistingEvents()
        {
            var isEndOfStream = false;
            var lastProcessEventNumber = 0;
            var batchSize = 100;

            while (!isEndOfStream)
            {
                var oldEvents = eventStoreConnection.ReadStreamEventsForwardAsync(
                    streamName,
                    lastProcessEventNumber,
                    batchSize,
                    true,
                    userCredentials).Result;

                foreach (var evt in oldEvents.Events)
                {
                    UpdateOrderState(evt, context);
                }

                lastProcessEventNumber += batchSize;
                isEndOfStream = oldEvents.IsEndOfStream;
            }
        }

        public void SubscribeToFutureEvents()
        {
            var thread = new Thread(() =>
            {
                eventStoreConnection.SubscribeToStreamAsync(streamName, true,
                        (sub, evt) =>
                        {
                            UpdateOrderState(evt, context);
                        })
                    .Wait();
            });

            thread.Start();
        }

        private void UpdateOrderState(ResolvedEvent @event, OrdersDbContext context)
        {
            var evt = @event.Event;

            var isEventFromDeletedStream = evt is null;
            if (isEventFromDeletedStream) return;

            var orderIdStr = evt.EventStreamId.Replace("order-", "");
            var orderId = new Guid(orderIdStr);

            var checkpoint = GetOrCreateOrderWithCheckpoint(orderId, context);
            var order = checkpoint.Order;

            order.Update(evt);
            checkpoint.LastProcessedEventNumber = evt.EventNumber;

            context.SaveChanges();
        }

        private OrderCheckpoint GetOrCreateOrderWithCheckpoint(Guid orderId, OrdersDbContext context)
        {
            var checkpoint = context.OrderCheckpoints
                .Include(checkpoint => checkpoint.Order)
                .FirstOrDefault(checkpoint => checkpoint.OrderId == orderId);

            if (checkpoint != null)
            {
                return checkpoint;
            }

            var order = new Order
            {
                Id = orderId
            };
            checkpoint = new OrderCheckpoint
            {
                Order = order,
                OrderId = order.Id
            };

            context.OrderCheckpoints.Add(checkpoint);

            return checkpoint;
        }
    }
}
