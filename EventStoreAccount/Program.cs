using System.Threading.Tasks;
using EsSample.Core;

namespace EventStoreAccount
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var context = new AccountDbContext();
            context.Database.EnsureCreated();

            using var connection = await EventStoreHelpers.CreateConnection();

            var stream = "account-ceda5c05-f314-4f04-91ad-1b8cad4d929e";

            var isEndOfStream = false;

            var lastProcessEventNumber = 0;
            var batchSize = 2;

            var state = new AccountState();

            while (!isEndOfStream)
            {
                var oldEvents = await connection.ReadStreamEventsForwardAsync(
                    stream,
                    lastProcessEventNumber,
                    batchSize,
                    false,
                    EventStoreHelpers.GetCredentials());

                foreach (var evt in oldEvents.Events)
                {
                    state.Update(evt);
                }

                lastProcessEventNumber += batchSize;

                isEndOfStream = oldEvents.IsEndOfStream;
            }
        }
    }
}
