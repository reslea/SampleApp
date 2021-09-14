using System;
using System.Text;
using EventStore.ClientAPI;
using Newtonsoft.Json.Linq;

namespace EventStoreAccount
{
    public class AccountState
    {
        public int Id { get; set; }

        public decimal MoneyAmount { get; set; }
        
        public void Update(ResolvedEvent evt)
        {
            var data = evt.Event.Data;

            var eventAmount = Encoding.UTF8.GetString(data);
            var amount = JObject.Parse(eventAmount)["amount"].ToObject<decimal>();

            if (evt.Event.EventType == "Receipt")
            {
                MoneyAmount += amount;

            } else if (evt.Event.EventType == "Withdrawal")
            {
                MoneyAmount -= amount;
            }

            Console.WriteLine($"currentl account state: {MoneyAmount}");
        }
    }
}