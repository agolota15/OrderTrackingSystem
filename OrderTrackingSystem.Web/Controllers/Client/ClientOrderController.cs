using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderTrackingSystem.Data;
using OrderTrackingSystem.Domain.Models;
using System.Security.Claims;

namespace OrderTrackingSystem.Web.Controllers.Client
{
    [Authorize(Roles = "Customer")]
    public class ClientOrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientOrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /ClientOrder
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _context.Orders
                .Where(o => o.CustomerId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
            return View(orders);
        }

        // GET: /ClientOrder/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == id && o.CustomerId == userId);
            if (order == null)
                return NotFound();

            return View(order);
        }

        // GET: /ClientOrder/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /ClientOrder/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            if (!ModelState.IsValid)
                return View(order);

            order.CustomerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            order.OrderDate = DateTime.Now;
            order.Status = "New";
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /ClientOrder/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == id && o.CustomerId == userId);
            if (order == null)
                return NotFound();

            return View(order);
        }

        // POST: /ClientOrder/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Order updated)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id != updated.Id)
                return NotFound();

            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == id && o.CustomerId == userId);
            if (order == null)
                return NotFound();

            if (!ModelState.IsValid)
                return View(updated);

            order.OrderNumber = updated.OrderNumber;
            order.Status = updated.Status;
            // Ewentualnie ogranicz edycję statusu przez klienta

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /ClientOrder/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == id && o.CustomerId == userId);
            if (order == null)
                return NotFound();

            return View(order);
        }

        // POST: /ClientOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == id && o.CustomerId == userId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
