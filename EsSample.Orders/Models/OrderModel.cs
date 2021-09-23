using System;
using System.Collections.Generic;
using System.Linq;

namespace EsSample.Orders.Models
{
    public class OrderModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public List<ProductModel> Products { get; set; } = new List<ProductModel>();

        public decimal TotalPrice => Products.Sum(p => p.Price);
    }
}
