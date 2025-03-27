using System.ComponentModel.DataAnnotations;

namespace OrderTrackingSystem.Domain.Models
{
    /// <summary>
    /// Wzorzec reklamacyjny definiowany przez klienta.
    /// </summary>
    public class ComplaintTemplate
    {
        public int Id { get; set; }

        [Required]
        public string TemplateTitle { get; set; }

        [Required]
        public string TemplateBody { get; set; }

        public string CustomerId { get; set; } // Który klient stworzył ten wzorzec
    }
}
