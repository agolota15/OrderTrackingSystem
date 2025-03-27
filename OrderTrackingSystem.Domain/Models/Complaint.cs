using System;
using System.ComponentModel.DataAnnotations;

namespace OrderTrackingSystem.Domain.Models
{
    public class Complaint
    {
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        public string CustomerId { get; set; } // kto zgłasza reklamację

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Pending"; // np. Pending, Approved, Rejected, InProgress
    }
}
