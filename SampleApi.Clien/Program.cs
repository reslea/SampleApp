using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SampleApi.Client
{
    class Program
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        static async Task Main(string[] args)
        {
            var result = await HttpClient.GetAsync("https://localhost:5001/api/Users");

            var body = await result.Content.ReadAsStringAsync();

            var users = JsonSerializer.Deserialize<List<User>>(body, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }

    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }
    }
}
