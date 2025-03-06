using Microsoft.EntityFrameworkCore;
using OrderTrackingSystem.Data;
using OrderTrackingSystem.Domain.Models;
using OrderTrackingSystem.Services;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OrderTrackingSystem.Tests
{
    public class OrderServiceTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldAddOrder()
        {
            var context = GetInMemoryDbContext();
            var service = new OrderService(context);
            var order = new Order
            {
                OrderNumber = "ORD001",
                CustomerName = "Jan Kowalski",
                Status = "Nowe"
            };

            await service.CreateOrderAsync(order);
            var orders = await service.GetAllOrdersAsync();

            Assert.Single(orders);
        }

        [Fact]
        public async Task UpdateOrderStatusAsync_ShouldUpdateStatusAndAddHistory()
        {
            var context = GetInMemoryDbContext();
            var service = new OrderService(context);
            var order = new Order
            {
                OrderNumber = "ORD002",
                CustomerName = "Anna Nowak",
                Status = "Nowe"
            };
            await service.CreateOrderAsync(order);
            await service.UpdateOrderStatusAsync(order.Id, "W trakcie");

            var updatedOrder = await service.GetOrderByIdAsync(order.Id);

            Assert.Equal("W trakcie", updatedOrder.Status);
            Assert.Single(updatedOrder.Histories);
            Assert.Equal("Nowe", updatedOrder.Histories.First().PreviousStatus);
            Assert.Equal("W trakcie", updatedOrder.Histories.First().NewStatus);
        }
    }
}
