using OrderTrackingSystem.Domain.Models;

namespace OrderTrackingSystem.Services.Interfaces
{
    public interface IShipmentService
    {
        Task<List<Shipment>> GetAllShipmentsAsync();
        Task<Shipment> GetShipmentByIdAsync(int id);
        Task CreateShipmentAsync(Shipment shipment);
        Task UpdateShipmentAsync(Shipment shipment);
        Task DeleteShipmentAsync(int id);
    }
}
