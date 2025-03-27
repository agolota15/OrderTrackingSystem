using System;
using System.ComponentModel.DataAnnotations;

namespace OrderTrackingSystem.Domain.Models
{
    /// <summary>
    /// Przesyłka powiązana z zamówieniem, wraz z rozszerzonymi statusami.
    /// </summary>
    public class Shipment
    {
        public int Id { get; set; }

        public int OrderId { get; set; } // powiązane zamówienie

        public string SenderId { get; set; } // kto wysyła (Customer lub Seller)

        [Required]
        public string ReceiverId { get; set; } // do kogo jest wysyłka

        public DateTime ShipmentDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Możliwe statusy: 
        /// - PreparedBySeller
        /// - AcceptedFromSender
        /// - AcceptedInBranch
        /// - ShippedFromBranch
        /// - DeliveredToPickup
        /// - ReadyForPickup
        /// - ReturnedToSender
        /// - PickedUp
        /// </summary>
        public string ShipmentStatus { get; set; } = "PreparedBySeller";

        /// <summary>
        /// Pole do ewentualnego śledzenia daty ostatniej zmiany statusu.
        /// </summary>
        public DateTime LastStatusUpdate { get; set; } = DateTime.Now;
    }
}
