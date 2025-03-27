using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderTrackingSystem.Data;
using OrderTrackingSystem.Domain.Models;
using System.Security.Claims;

namespace OrderTrackingSystem.Web.Controllers.Client
{
    [Authorize(Roles = "Customer")]
    public class ClientShipmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientShipmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /ClientShipment
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // Pokaż przesyłki, w których user jest nadawcą lub odbiorcą
            var shipments = await _context.Shipments
                .Where(s => s.SenderId == userId || s.ReceiverId == userId)
                .OrderByDescending(s => s.ShipmentDate)
                .ToListAsync();
            return View(shipments);
        }

        // GET: /ClientShipment/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var shipment = await _context.Shipments
                .FirstOrDefaultAsync(s => s.Id == id &&
                     (s.SenderId == userId || s.ReceiverId == userId));
            if (shipment == null)
                return NotFound();

            return View(shipment);
        }

        // GET: /ClientShipment/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /ClientShipment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Shipment shipment)
        {
            if (!ModelState.IsValid)
                return View(shipment);

            // Zakładamy, że user jest nadawcą
            shipment.SenderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            shipment.ShipmentDate = DateTime.Now;
            shipment.ShipmentStatus = "AcceptedFromSender";
            _context.Shipments.Add(shipment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /ClientShipment/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var shipment = await _context.Shipments
                .FirstOrDefaultAsync(s => s.Id == id && s.SenderId == userId);
            // Tylko nadawca może edytować
            if (shipment == null)
                return NotFound();

            return View(shipment);
        }

        // POST: /ClientShipment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Shipment updated)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id != updated.Id)
                return NotFound();

            var shipment = await _context.Shipments
                .FirstOrDefaultAsync(s => s.Id == id && s.SenderId == userId);
            if (shipment == null)
                return NotFound();

            if (!ModelState.IsValid)
                return View(updated);

            shipment.ReceiverId = updated.ReceiverId;
            // Edycja statusu przez klienta? Zależy od logiki
            // shipment.ShipmentStatus = updated.ShipmentStatus;

            _context.Shipments.Update(shipment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /ClientShipment/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var shipment = await _context.Shipments
                .FirstOrDefaultAsync(s => s.Id == id && s.SenderId == userId);
            if (shipment == null)
                return NotFound();

            return View(shipment);
        }

        // POST: /ClientShipment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var shipment = await _context.Shipments
                .FirstOrDefaultAsync(s => s.Id == id && s.SenderId == userId);
            if (shipment != null)
            {
                _context.Shipments.Remove(shipment);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
