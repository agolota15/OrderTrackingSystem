using Microsoft.EntityFrameworkCore;
using OrderTrackingSystem.Data;
using OrderTrackingSystem.Domain.Models;
using OrderTrackingSystem.Services.Interfaces;

namespace OrderTrackingSystem.Services.Implementations
{
    public class ShipmentService : IShipmentService
    {
        private readonly ApplicationDbContext _context;
        public ShipmentService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Shipment>> GetAllShipmentsAsync() =>
            await _context.Shipments.ToListAsync();
        public async Task<Shipment> GetShipmentByIdAsync(int id) =>
            await _context.Shipments.FirstOrDefaultAsync(s => s.Id == id);
        public async Task CreateShipmentAsync(Shipment shipment)
        {
            _context.Shipments.Add(shipment);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateShipmentAsync(Shipment shipment)
        {
            _context.Shipments.Update(shipment);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteShipmentAsync(int id)
        {
            var shipment = await GetShipmentByIdAsync(id);
            if (shipment != null)
            {
                _context.Shipments.Remove(shipment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
