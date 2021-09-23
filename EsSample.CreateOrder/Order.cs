using System;
using System.Collections.Generic;
using System.Linq;

namespace EsSample.CreateOrder
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public List<Product> Products { get; set; } = new List<Product>();

        public decimal TotalPrice => Products.Sum(p => p.Price);
    }
}
