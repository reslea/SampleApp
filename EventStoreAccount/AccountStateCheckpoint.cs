namespace EventStoreAccount
{
    public class AccountStateCheckpoint
    {
        public int Id { get; set; }

        public int AccountStateId { get; set; }
        
        public AccountState AccountState { get; set; }

        public int LastProcessedEventNumber { get; set; }
    }
}