/*using Microsoft.EntityFrameworkCore;
using OrderTrackingSystem.Data;
using OrderTrackingSystem.Domain.Models;
using OrderTrackingSystem.Services;
<<<<<<< HEAD
using System.Linq;
using System.Threading.Tasks;
using Xunit;
=======
using Xunit;
using System.Threading.Tasks;
using System.Linq;
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5

namespace OrderTrackingSystem.Tests
{
    public class OrderServiceTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
<<<<<<< HEAD
                .UseInMemoryDatabase(databaseName: "TestDb")
=======
                .UseInMemoryDatabase(databaseName: "OrderTrackingSystemDatabase")
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldAddOrder()
        {
<<<<<<< HEAD
=======
            // Arrange
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
            var context = GetInMemoryDbContext();
            var service = new OrderService(context);
            var order = new Order
            {
                OrderNumber = "ORD001",
                CustomerName = "Jan Kowalski",
                Status = "Nowe"
            };

<<<<<<< HEAD
            await service.CreateOrderAsync(order);
            var orders = await service.GetAllOrdersAsync();

=======
            // Act
            await service.CreateOrderAsync(order);
            var orders = await service.GetAllOrdersAsync();

            // Assert
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
            Assert.Single(orders);
        }

        [Fact]
        public async Task UpdateOrderStatusAsync_ShouldUpdateStatusAndAddHistory()
        {
<<<<<<< HEAD
=======
            // Arrange
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
            var context = GetInMemoryDbContext();
            var service = new OrderService(context);
            var order = new Order
            {
                OrderNumber = "ORD002",
                CustomerName = "Anna Nowak",
                Status = "Nowe"
            };
            await service.CreateOrderAsync(order);
<<<<<<< HEAD
            await service.UpdateOrderStatusAsync(order.Id, "W trakcie");

            var updatedOrder = await service.GetOrderByIdAsync(order.Id);

=======

            // Act
            await service.UpdateOrderStatusAsync(order.Id, "W trakcie");
            var updatedOrder = await service.GetOrderByIdAsync(order.Id);

            // Assert
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
            Assert.Equal("W trakcie", updatedOrder.Status);
            Assert.Single(updatedOrder.Histories);
            Assert.Equal("Nowe", updatedOrder.Histories.First().PreviousStatus);
            Assert.Equal("W trakcie", updatedOrder.Histories.First().NewStatus);
        }
    }
}
*/