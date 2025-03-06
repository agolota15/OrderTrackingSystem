using System;
using System.ComponentModel.DataAnnotations;

namespace OrderTrackingSystem.Domain.Models
{
    public class OrderHistory
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        [Display(Name = "Poprzedni status")]
        public string PreviousStatus { get; set; }

        [Display(Name = "Nowy status")]
        public string NewStatus { get; set; }

        [Display(Name = "Data zmiany")]
        public DateTime ChangeDate { get; set; } = DateTime.Now;

        // Nawigacja do zamówienia
        public Order Order { get; set; }
    }
}
