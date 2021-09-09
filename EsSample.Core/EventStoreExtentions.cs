using System;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;

namespace EsSample.Core
{
    public static class EventStoreExtentions
    {
        public static async Task<EventStoreSubscription> SubscribeToStream(
            this IEventStoreConnection connection,
            string stream,
            Action<EventStoreSubscription, ResolvedEvent> processEvent)
        {
            return await connection.SubscribeToStreamAsync(
                stream,
                false,
                processEvent,
                null,
                new UserCredentials("admin", "changeit"));
        }

        public static async Task<EventStoreSubscription> SubscribeToLinkedStream(
            this IEventStoreConnection connection,
            string stream,
            Action<EventStoreSubscription, ResolvedEvent> processEvent)
        {
            return await connection.SubscribeToStreamAsync(
                stream,
                true,
                processEvent,
                null,
                new UserCredentials("admin", "changeit"));
        }
    }
}
