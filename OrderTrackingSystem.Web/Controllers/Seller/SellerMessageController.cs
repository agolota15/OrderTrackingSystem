using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderTrackingSystem.Data;
using OrderTrackingSystem.Domain.Models;

namespace OrderTrackingSystem.Web.Controllers.Seller
{
    [Authorize(Roles = "Seller")]
    public class SellerMessageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SellerMessageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /SellerMessage/Inbox
        public async Task<IActionResult> Inbox()
        {
            // Sprzedawca widzi np. wszystkie wiadomości do roli "Seller"
            // Albo do jakiegoś dedykowanego userId
            var messages = await _context.Messages
                .Where(m => m.ToUserId == null) // lub inna logika
                .OrderByDescending(m => m.SentDate)
                .ToListAsync();
            return View(messages);
        }

        // GET: /SellerMessage/Outbox
        public async Task<IActionResult> Outbox()
        {
            // Załóżmy, że sprzedawca ma userId
            // W realnym scenariuszu należałoby go pobrać z kontekstu
            var sellerId = "someSellerId"; // lub pobrać z claims
            var messages = await _context.Messages
                .Where(m => m.FromUserId == sellerId)
                .OrderByDescending(m => m.SentDate)
                .ToListAsync();
            return View(messages);
        }

        // GET: /SellerMessage/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
                return NotFound();
            return View(message);
        }

        // GET: /SellerMessage/Create
        public IActionResult Create() => View();

        // POST: /SellerMessage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Message msg)
        {
            if (!ModelState.IsValid)
                return View(msg);

            // Zakładamy, że sprzedawca ma userId:
            // var sellerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // msg.FromUserId = sellerId;
            msg.SentDate = DateTime.Now;
            msg.IsRead = false;

            _context.Messages.Add(msg);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Outbox));
        }

        // Analogicznie: Edit, Delete, etc. – zależnie od potrzeb
    }
}
