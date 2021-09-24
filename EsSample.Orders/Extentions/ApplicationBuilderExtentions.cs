using EsSample.Orders.Database;
using EsSample.Orders.Database.Entities;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EsSample.Orders.Extentions
{
    public static class ApplicationBuilderExtentions
    {
        public static void EnableOrderStateSyncronisation(this IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

            using var scope = scopeFactory.CreateScope();
            var esConnection = scope.ServiceProvider.GetService<IEventStoreConnection>();
            var esCredentials = scope.ServiceProvider.GetService<UserCredentials>();

            var context = scope.ServiceProvider.GetService<OrdersDbContext>();

            var streamName = "$ce-order";

            ProcessExistingEvents(streamName, context, esConnection, esCredentials);
            SubscribeToFutureEvents(streamName, esConnection, context);
        }

        private static void SubscribeToFutureEvents(string streamName, IEventStoreConnection esConnection, OrdersDbContext context)
        {
            Task.Run(() => esConnection.SubscribeToStreamAsync(streamName, true, (sub, evt) =>
            {
                UpdateOrderState(evt, context);
            }));
        }

        private static void ProcessExistingEvents(string streamName, OrdersDbContext context, IEventStoreConnection esConnection, UserCredentials esCredentials)
        {
            var isEndOfStream = false;
            var lastProcessEventNumber = 0;

            while (!isEndOfStream)
            {
                var oldEvents = esConnection.ReadStreamEventsForwardAsync(
                    streamName,
                    lastProcessEventNumber,
                    1000,
                    true,
                    esCredentials).Result;

                foreach (var evt in oldEvents.Events)
                {
                    UpdateOrderState(evt, context);
                }
            }
        }

        private static void UpdateOrderState(ResolvedEvent @event, OrdersDbContext context)
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

        private static OrderCheckpoint GetOrCreateOrderWithCheckpoint(Guid orderId, OrdersDbContext context)
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
