using Microsoft.AspNetCore.Mvc;
using OrderTrackingSystem.Domain.Models;
using OrderTrackingSystem.Services.Interfaces;
<<<<<<< HEAD
using System;
=======
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
using System.Threading.Tasks;

namespace OrderTrackingSystem.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
<<<<<<< HEAD
=======

>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

<<<<<<< HEAD
        // Lista zamówień z filtrowaniem
=======
        // GET: /Order
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
        public async Task<IActionResult> Index(string search, string status, DateTime? startDate, DateTime? endDate)
        {
            var orders = await _orderService.GetAllOrdersAsync(search, status, startDate, endDate);
            return View(orders);
        }

<<<<<<< HEAD
        // Szczegóły zamówienia
=======
        // GET: /Order/Details/5
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();
            return View(order);
        }

<<<<<<< HEAD
        // Tworzenie zamówienia
=======
        // GET: /Order/Create
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
        public IActionResult Create()
        {
            return View();
        }

<<<<<<< HEAD
=======
        // POST: /Order/Create
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            if (ModelState.IsValid)
            {
                await _orderService.CreateOrderAsync(order);
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

<<<<<<< HEAD
        // Edycja zamówienia
=======
        // GET: /Order/Edit/5
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();
            return View(order);
        }

<<<<<<< HEAD
=======
        // POST: /Order/Edit/5
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                await _orderService.UpdateOrderAsync(order);
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

<<<<<<< HEAD
        // Usuwanie zamówienia
=======
        // GET: /Order/Delete/5
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();
            return View(order);
        }

<<<<<<< HEAD
=======
        // POST: /Order/Delete/5
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
