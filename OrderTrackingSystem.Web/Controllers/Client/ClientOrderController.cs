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
            var order = await GetOrderIfOwnerAsync(id);
            if (order == null)
            {
                return NotFound();
            }

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
        public async Task<IActionResult> Create([Bind("OrderNumber")] Order order)
        {
            if (!ModelState.IsValid)
                return View(order);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            order.CustomerId = userId;
            order.OrderDate = DateTime.Now;
            order.Status = "New";

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: /ClientOrder/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var order = await GetOrderIfOwnerAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: /ClientOrder/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Order updated)
        {
            // Sprawdź, czy ID z URL-a i obiektu przekazanego w formularzu się zgadzają
            if (id != updated.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(updated);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == id && o.CustomerId == userId);

            if (order == null)
            {
                return NotFound();
            }

            // Aktualizacja tylko wybranych pól (chronimy się przed manipulacją innymi polami)
            order.OrderNumber = updated.OrderNumber;
            order.Status = updated.Status;
            // Jeżeli nie chcesz, by klient samodzielnie zmieniał Status, usuń powyższą linię.


            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                // Sprawdzamy, czy obiekt wciąż istnieje (być może ktoś go usunął)
                if (!await OrderExists(updated.Id, userId))
                {
                    return NotFound();
                }
                else
                {
                    // Tutaj mamy konflikt zapisu (rekord został zmieniony w międzyczasie)
                    ModelState.AddModelError(string.Empty, "Wystąpił konflikt zapisu. Spróbuj ponownie.");
                    return View(updated);
                }
            }
        }

        // GET: /ClientOrder/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var order = await GetOrderIfOwnerAsync(id);
            if (order == null)
            {
                return NotFound();
            }

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

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError(string.Empty, "Wystąpił konflikt zapisu podczas usuwania.");
                    return View(order);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Metoda pomocnicza, zwracająca zamówienie, tylko jeśli należy do obecnie zalogowanego użytkownika
        /// </summary>
        private async Task<Order?> GetOrderIfOwnerAsync(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == id && o.CustomerId == userId);
        }

        /// <summary>
        /// Sprawdza, czy zamówienie istnieje (pomocne w obsłudze wyjątków concurrency)
        /// </summary>
        private async Task<bool> OrderExists(int id, string userId)
        {
            return await _context.Orders.AnyAsync(o => o.Id == id && o.CustomerId == userId);
        }
    }
}
