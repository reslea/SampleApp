using System;

namespace EventStoreAccount
{
    public class AccountStateCheckpoint
    {
        public int Id { get; set; }

        public Guid AccountStateId { get; set; }
        
        public AccountState AccountState { get; set; }

        public long LastProcessedEventNumber { get; set; }
    }
}