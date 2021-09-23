using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EsSample.Core;
using EventStore.ClientAPI;
using EventStore.ClientAPI.Common.Log;
using EventStore.ClientAPI.Projections;
using EventStore.ClientAPI.SystemData;

namespace EsSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var connection = await EventStoreHelpers.CreateConnection();

            var stream = "my-first-stream";

            await connection.SubscribeToStream(stream, ProcessEvent);

            Console.WriteLine("processing events");
            Console.ReadLine();
            var pm = EventStoreHelpers.CreateProjectionsManager();
            var creds = EventStoreHelpers.GetCredentials();

            var projection = "money-for-accounts";
            var partitionId = "account-e3f62a80-a504-4cfb-97f1-b9a003535da8";
            var result = await pm.GetPartitionResultAsync(projection, partitionId, creds);
            Console.WriteLine(result);

            // projection create from file sample
            //var fileStr = await File.ReadAllTextAsync("test.js");
            //await pm.CreateContinuousAsync("test", fileStr);
        }

        static void ProcessEvent(
            EventStoreSubscription subscription, 
            ResolvedEvent resolvedEvent)
        {
            var evt = resolvedEvent.Event;

            Console.WriteLine(evt.EventStreamId);
            Console.WriteLine(evt.EventType);

            var jsonData = Encoding.UTF8.GetString(evt.Data);
            Console.WriteLine(jsonData);
        }
    }
}
