using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderTrackingSystem.Data;
using OrderTrackingSystem.Domain.Models;
using OrderTrackingSystem.Services.Interfaces;

namespace OrderTrackingSystem.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllOrdersAsync(string search = null, string status = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _context.Orders.Include(o => o.Histories)
                                       .Include(o => o.OrderItems)
                                       .AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(o => o.OrderNumber.Contains(search) || o.CustomerName.Contains(search));
            if (!string.IsNullOrEmpty(status))
                query = query.Where(o => o.Status == status);
            if (startDate.HasValue)
                query = query.Where(o => o.OrderDate >= startDate.Value);
            if (endDate.HasValue)
                query = query.Where(o => o.OrderDate <= endDate.Value);

            return await query.ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.Include(o => o.Histories)
                                        .Include(o => o.OrderItems)
                                        .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task CreateOrderAsync(Order order)
        {
            if (string.IsNullOrWhiteSpace(order.TrackingNumber))
            {
                order.TrackingNumber = GenerateTrackingNumber();
            }
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateOrderStatusAsync(int id, string newStatus)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                var history = new OrderHistory
                {
                    OrderId = id,
                    PreviousStatus = order.Status,
                    NewStatus = newStatus,
                    ChangeDate = DateTime.Now
                };

                order.Status = newStatus;
                _context.Add(history);
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
            }
        }

        private string GenerateTrackingNumber()
        {
            // Prosty generator oparty na GUID – możesz rozbudować logikę, np. dodać prefix, datę itp.
            return "TRK-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        }
    }
}
