using System;
using System.ComponentModel.DataAnnotations;

namespace OrderTrackingSystem.Domain.Models
{
    public class Voucher
    {
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        public decimal DiscountValue { get; set; } // np. kwota rabatu

        public DateTime ExpirationDate { get; set; }

        public bool IsUsed { get; set; } = false;
    }
}
