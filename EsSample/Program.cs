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

            var result = await pm.GetResultAsync("money-for-account-ceda5c05-f314-4f04-91ad-1b8cad4d929e", creds);
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
