using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using EventStore.ClientAPI.Common.Log;
using EventStore.ClientAPI.Projections;
using EventStore.ClientAPI.SystemData;
using Newtonsoft.Json;

namespace EsSample.Core
{
    public static class EventStoreHelpers
    {
        public static async Task<IEventStoreConnection> CreateConnection()
        {
            var connection = EventStoreConnection.Create(
                new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1113));

            await connection.ConnectAsync();
            return connection;
        }

        public static EventData CreateEvent(string type, object dataObj, object metadataObj = null)
        {
            var dataBytes = Encoding.UTF8.GetBytes(
                JsonConvert.SerializeObject(dataObj));

            var metadataBytes = metadataObj == null
                ? null
                : Encoding.UTF8.GetBytes(
                    JsonConvert.SerializeObject(metadataObj));

            return new EventData(Guid.NewGuid(), type, true, dataBytes, metadataBytes);
        }

        public static ProjectionsManager CreateProjectionsManager()
        {
            return new ProjectionsManager(
                new ConsoleLogger(),
                new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2113), 
                TimeSpan.FromSeconds(30));
        }

        public static UserCredentials GetCredentials()
        {
            return new UserCredentials("admin", "changeit");
        }
    }
}
