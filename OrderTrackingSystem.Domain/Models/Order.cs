using System;
using System.ComponentModel.DataAnnotations;

namespace OrderTrackingSystem.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string OrderNumber { get; set; }

        [Required]
        public string CustomerId { get; set; } // ID użytkownika (IdentityUser.Id)

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public string Status { get; set; } = "New"; // np. New, InProgress, Shipped, Completed
    }
}
