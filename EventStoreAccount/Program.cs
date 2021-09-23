using System;
using System.Threading;
using System.Threading.Tasks;
using EsSample.Core;
using EventStore.ClientAPI;
using Microsoft.EntityFrameworkCore;

namespace EventStoreAccount
{
    class Program
    {
        private static readonly string StreamName = "$ce-account";

        static async Task Main(string[] args)
        {
            #pragma warning disable 4014
            Task.Run(async () => await RunStateChecker());
            #pragma warning restore 4014

            using var connection = await EventStoreHelpers.CreateConnection();

            await using var context = new AccountDbContext();

            await Task.Delay(TimeSpan.FromSeconds(3));
            
            //var withdrawAmount = 580;

            //var accountCheckpoint = await context
            //    .AccountStateCheckpoints
            //    .Include(checkpoint => checkpoint.AccountState)
            //    .FirstOrDefaultAsync(checkpoint => checkpoint.AccountStateId == AccountGuid);

            //if (accountCheckpoint.AccountState.MoneyAmount - withdrawAmount >= 0)
            //{
            //    var withdrawEvt = EventStoreHelpers.CreateEvent("Withdrawal", new
            //    {
            //        amount = withdrawAmount
            //    });

            //    var expectedEventNumber = accountCheckpoint.LastProcessedEventNumber;

            //    await connection.AppendToStreamAsync(StreamName, expectedEventNumber, withdrawEvt);
            //}

            Console.ReadLine();
        }

        static async Task RunStateChecker()
        {
            using var context = new AccountDbContext();
            await context.Database.EnsureCreatedAsync();

            using var connection = await EventStoreHelpers.CreateConnection();

            await ProcessLastEvents(connection, context);
        }

        static async Task ProcessLastEvents(IEventStoreConnection connection, AccountDbContext context)
        {
            var isEndOfStream = false;
            var lastProcessEventNumber = 0;
            var batchSize = 2;

            while (!isEndOfStream)
            {
                var oldEvents = await connection.ReadStreamEventsForwardAsync(
                    StreamName,
                    lastProcessEventNumber,
                    batchSize,
                    true,
                    EventStoreHelpers.GetCredentials());

                await UpdateState(oldEvents, context);

                lastProcessEventNumber += batchSize;

                isEndOfStream = oldEvents.IsEndOfStream;
            }
        }

        static async Task UpdateState(StreamEventsSlice oldEvents, AccountDbContext context)
        {
            foreach (var evt in oldEvents.Events)
            {
                if(evt.Event is null) continue;

                var accountIdStr = evt.Event.EventStreamId.Replace("account-", "");
                var accountId = new Guid(accountIdStr);

                var accountStateCheckpoint = await GetOrCreateAccount(accountId, context);
                var accountState = accountStateCheckpoint.AccountState;

                accountState.Update(evt);
                accountStateCheckpoint.LastProcessedEventNumber = evt.Event.EventNumber;

                await context.SaveChangesAsync();
            }
        }

        static async Task<AccountStateCheckpoint> GetOrCreateAccount(Guid accountId, AccountDbContext context)
        {
            var accountStateCheckpoint = await context.AccountStateCheckpoints
                .Include(checkpoint => checkpoint.AccountState)
                .FirstOrDefaultAsync(checkpoint => checkpoint.AccountStateId == accountId);

            if (accountStateCheckpoint != null)
            {
                return accountStateCheckpoint;
            }

            var accountState = new AccountState
            {
                Id = accountId
            };
            accountStateCheckpoint = new AccountStateCheckpoint
            {
                AccountState = accountState
            };

            await context.AccountStateCheckpoints.AddAsync(accountStateCheckpoint);

            return accountStateCheckpoint;
        }
    }
}
