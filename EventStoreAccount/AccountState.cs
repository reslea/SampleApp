using System;
using System.Text;
using EventStore.ClientAPI;
using Newtonsoft.Json.Linq;

namespace EventStoreAccount
{
    public class AccountState
    {
        public Guid Id { get; set; }

        public decimal MoneyAmount { get; set; }
        
        public void Update(ResolvedEvent evt)
        {
            var data = evt.Event.Data;

            var eventAmount = Encoding.UTF8.GetString(data);
            var amount = JObject.Parse(eventAmount)["amount"].ToObject<decimal>();

            switch (evt.Event.EventType)
            {
                case "Receipt":
                    MoneyAmount += amount;
                    break;
                case "Withdrawal":
                    MoneyAmount -= amount;
                    break;
            }

            Console.WriteLine($"current account state: {MoneyAmount}");
        }
    }
}