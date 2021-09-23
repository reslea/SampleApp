using System;

namespace EsSample.Orders.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; }

        public decimal Price { get; set; }

    }
}
