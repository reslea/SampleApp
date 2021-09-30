using EsSample.Orders.Database;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;

namespace EsSample.Orders.OrderSync
{
    public interface IOrderDbSyncronizer
    {
        void ProcessExistingEvents();
        void SubscribeToFutureEvents();
    }
}