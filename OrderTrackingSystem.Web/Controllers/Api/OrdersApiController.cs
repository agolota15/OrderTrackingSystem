using Microsoft.AspNetCore.Mvc;
using OrderTrackingSystem.Domain.Models;
using OrderTrackingSystem.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderTrackingSystem.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersApiController : ControllerBase
    {
        private readonly IOrderService _orderService;
<<<<<<< HEAD
=======

>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
        public OrdersApiController(IOrderService orderService)
        {
            _orderService = orderService;
        }

<<<<<<< HEAD
=======
        // GET: api/OrdersApi
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

<<<<<<< HEAD
=======
        // GET: api/OrdersApi/5
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

<<<<<<< HEAD
=======
        // PUT: api/OrdersApi/5/status
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string newStatus)
        {
            await _orderService.UpdateOrderStatusAsync(id, newStatus);
            return NoContent();
        }
    }
}
