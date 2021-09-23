using EsSample.Orders.Database.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace EsSample.Orders.Database
{
    public class OrderCheckpoint
    {
        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        public long LastProcessedEventNumber { get; set; }
    }
}