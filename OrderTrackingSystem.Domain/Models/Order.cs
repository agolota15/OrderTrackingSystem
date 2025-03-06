using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrderTrackingSystem.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Numer zamówienia")]
        public string OrderNumber { get; set; }

        [Required]
        [Display(Name = "Nazwa klienta")]
        public string CustomerName { get; set; }

        [Display(Name = "Data zamówienia")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Status zamówienia")]
        public string Status { get; set; }

        [Display(Name = "Adres wysyłki")]
        public string ShippingAddress { get; set; }

        [Display(Name = "Numer przesyłki")]
        public string TrackingNumber { get; set; }

        // Lista pozycji zamówienia
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        // Historia zmian statusu
        public ICollection<OrderHistory> Histories { get; set; } = new List<OrderHistory>();
    }
}
