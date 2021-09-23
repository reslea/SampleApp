using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EsSample.Core;
using EventStore.ClientAPI;
using Newtonsoft.Json;

namespace EsSample.CreateOrder
{
    class Program
    {
        private static HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            var order = new Order
            {
                Products = new List<Product>
                {
                    new Product {Title = "Пицца", Price = 100},
                    new Product {Title = "Хот-дог", Price = 30},
                }
            };

            var jsonOrder = JsonConvert.SerializeObject(order);

            //var result = await client.PostAsync("API_LINK", new StringContent(jsonOrder, Encoding.UTF8));

            using var connection = await EventStoreHelpers.CreateConnection();

            var streamName = $"order-{order.Id}";

            var evt = EventStoreHelpers.CreateEvent(nameof(EventType.OrderCreated), order);

            await Task.Delay(TimeSpan.FromSeconds(3));
            await connection.AppendToStreamAsync(streamName, ExpectedVersion.Any, evt);
        }
    }
}
