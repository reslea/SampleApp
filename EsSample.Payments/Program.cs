using System;
using System.Text;
using System.Threading.Tasks;
using EsSample.Core;
using EventStore.ClientAPI;
using Console = System.Console;

namespace EsSample.Payments
{
    class Program
    {
        private static IEventStoreConnection _connection;
        private static Random _rnd = new Random();

        static async Task Main(string[] args)
        {
            _connection = await EventStoreHelpers.CreateConnection();

            var stream = "$et-OrderCreated";

            await _connection.SubscribeToLinkedStream(stream, ProcessEvent);

            Console.WriteLine("processing events");
            Console.ReadLine();
        }

        static void ProcessEvent(
            EventStoreSubscription subscription,
            ResolvedEvent resolvedEvent)
        {
            var evt = resolvedEvent.Event;

            var jsonData = Encoding.UTF8.GetString(evt.Data);
            var stars = new string('*', 8);

            Console.WriteLine(jsonData.Replace(Environment.NewLine, ""));
            Console.WriteLine(stars);

            if (_rnd.Next(1, 100) > 50)
            {
                _connection.AppendToStreamAsync(evt.EventStreamId, ExpectedVersion.Any,
                    new EventData
                    (Guid.NewGuid(),
                        nameof(EventType.OrderPaymentSuccess),
                        true,
                        evt.Data,
                        evt.Metadata));

                Console.WriteLine("payment succeeded");
            }
            else
            {
                _connection.AppendToStreamAsync(evt.EventStreamId, ExpectedVersion.Any,
                    new EventData
                    (Guid.NewGuid(),
                        nameof(EventType.OrderPaymentFailure),
                        true,
                        evt.Data,
                        evt.Metadata));
                Console.WriteLine("payment rejected");
            }
        }
    }
}
