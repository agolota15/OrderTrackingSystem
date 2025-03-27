using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderTrackingSystem.Data;
using OrderTrackingSystem.Domain.Models;
using System.Security.Claims;

namespace OrderTrackingSystem.Web.Controllers.Client
{
    [Authorize(Roles = "Customer")]
    public class ClientMessageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientMessageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /ClientMessage/Inbox
        public async Task<IActionResult> Inbox()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var messages = await _context.Messages
                .Where(m => m.ToUserId == userId)
                .OrderByDescending(m => m.SentDate)
                .ToListAsync();
            return View(messages);
        }

        // GET: /ClientMessage/Outbox
        public async Task<IActionResult> Outbox()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var messages = await _context.Messages
                .Where(m => m.FromUserId == userId)
                .OrderByDescending(m => m.SentDate)
                .ToListAsync();
            return View(messages);
        }

        // GET: /ClientMessage/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var message = await _context.Messages
                .FirstOrDefaultAsync(m => m.Id == id &&
                     (m.FromUserId == userId || m.ToUserId == userId));
            if (message == null)
                return NotFound();

            return View(message);
        }

        // GET: /ClientMessage/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /ClientMessage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Message message)
        {
            if (!ModelState.IsValid)
                return View(message);

            message.FromUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            message.SentDate = DateTime.Now;
            message.IsRead = false;

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Outbox));
        }

        // Brak typowego Edit/Delete – zależnie od potrzeb.
        // Możesz dodać je analogicznie, jeśli chcesz, by klient mógł edytować/usunąć wysłaną wiadomość.
    }
}
