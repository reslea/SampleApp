using System;

namespace EsSample.CreateOrder
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; }

        public decimal Price { get; set; }

    }
}
