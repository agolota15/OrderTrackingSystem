using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderTrackingSystem.Data;
using OrderTrackingSystem.Domain.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OrderTrackingSystem.Web.Controllers.Seller
{
    [Authorize(Roles = "Seller")]
    public class SellerShipmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SellerShipmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SellerShipment/Index
        public async Task<IActionResult> Index()
        {
            var shipments = await _context.Shipments
                .OrderByDescending(s => s.ShipmentDate)
                .ToListAsync();
            return View(shipments);
        }

        // GET: SellerShipment/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var shipment = await _context.Shipments.FirstOrDefaultAsync(s => s.Id == id);
            if (shipment == null)
            {
                return NotFound();
            }
            return View(shipment);
        }

        // GET: SellerShipment/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SellerShipment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Shipment shipment)
        {
            if (ModelState.IsValid)
            {
                // W tym przypadku, jako sprzedawca, ustawiamy SenderId na ID aktualnie zalogowanego użytkownika.
                shipment.SenderId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
                shipment.ShipmentDate = System.DateTime.Now;
                shipment.LastStatusUpdate = System.DateTime.Now;
                // Domyślny status – możesz ustawić np. "PreparedBySeller" lub inny
                shipment.ShipmentStatus = "PreparedBySeller";

                _context.Shipments.Add(shipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shipment);
        }

        // GET: SellerShipment/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var shipment = await _context.Shipments.FindAsync(id);
            if (shipment == null)
            {
                return NotFound();
            }
            return View(shipment);
        }

        // POST: SellerShipment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Shipment shipment)
        {
            if (id != shipment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Aktualizujemy także datę ostatniej modyfikacji
                    shipment.LastStatusUpdate = System.DateTime.Now;
                    _context.Shipments.Update(shipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipmentExists(shipment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(shipment);
        }

        // GET: SellerShipment/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var shipment = await _context.Shipments.FirstOrDefaultAsync(s => s.Id == id);
            if (shipment == null)
            {
                return NotFound();
            }
            return View(shipment);
        }

        // POST: SellerShipment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shipment = await _context.Shipments.FindAsync(id);
            if (shipment != null)
            {
                _context.Shipments.Remove(shipment);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ShipmentExists(int id)
        {
            return _context.Shipments.Any(e => e.Id == id);
        }
    }
}
