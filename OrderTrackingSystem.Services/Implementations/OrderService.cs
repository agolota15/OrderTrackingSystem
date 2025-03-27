﻿using Microsoft.EntityFrameworkCore;
using OrderTrackingSystem.Data;
using OrderTrackingSystem.Domain.Models;
using OrderTrackingSystem.Services.Interfaces;

namespace OrderTrackingSystem.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetAllOrdersAsync() =>
            await _context.Orders.ToListAsync();
        public async Task<Order> GetOrderByIdAsync(int id) =>
            await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        public async Task CreateOrderAsync(Order order)
        {
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
            var order = await GetOrderByIdAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
