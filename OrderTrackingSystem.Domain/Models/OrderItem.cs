using System.ComponentModel.DataAnnotations;

namespace OrderTrackingSystem.Domain.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
<<<<<<< HEAD
=======

>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
        public int OrderId { get; set; }

        [Required]
        [Display(Name = "Nazwa produktu")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Ilość")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Cena")]
        public decimal Price { get; set; }

        // Nawigacja do zamówienia
        public Order Order { get; set; }
    }
}
