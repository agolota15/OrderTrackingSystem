using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderTrackingSystem.Data;

namespace OrderTrackingSystem.Web.Controllers.Seller
{
    [Authorize(Roles = "Seller")]
    public class SellerProcessController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SellerProcessController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /SellerProcess
        public IActionResult Index()
        {
            // Może wyświetlać stronę z przyciskiem "Uruchom proces"
            return View();
        }

        // POST: /SellerProcess/RunAutoUpdate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RunAutoUpdate()
        {
            // Przykład: automatyczna zmiana statusu przesyłek
            var shipments = await _context.Shipments
                .Where(s => s.ShipmentStatus == "AcceptedInBranch")
                .ToListAsync();

            foreach (var s in shipments)
            {
                // np. zmieniamy status
                s.ShipmentStatus = "ShippedFromBranch";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
