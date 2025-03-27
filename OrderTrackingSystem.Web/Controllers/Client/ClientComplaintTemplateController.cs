using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderTrackingSystem.Data;
using OrderTrackingSystem.Domain.Models;
using System.Security.Claims;

namespace OrderTrackingSystem.Web.Controllers.Client
{
    [Authorize(Roles = "Customer")]
    public class ClientComplaintTemplateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientComplaintTemplateController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var templates = await _context.ComplaintTemplates
                .Where(t => t.CustomerId == userId)
                .ToListAsync();
            return View(templates);
        }

        public async Task<IActionResult> Details(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var template = await _context.ComplaintTemplates
                .FirstOrDefaultAsync(t => t.Id == id && t.CustomerId == userId);
            if (template == null)
                return NotFound();
            return View(template);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ComplaintTemplate template)
        {
            if (!ModelState.IsValid)
                return View(template);

            template.CustomerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.ComplaintTemplates.Add(template);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var template = await _context.ComplaintTemplates
                .FirstOrDefaultAsync(t => t.Id == id && t.CustomerId == userId);
            if (template == null)
                return NotFound();

            return View(template);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ComplaintTemplate template)
        {
            if (id != template.Id)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existing = await _context.ComplaintTemplates
                .FirstOrDefaultAsync(t => t.Id == id && t.CustomerId == userId);
            if (existing == null)
                return NotFound();

            if (!ModelState.IsValid)
                return View(template);

            existing.TemplateTitle = template.TemplateTitle;
            existing.TemplateBody = template.TemplateBody;

            _context.ComplaintTemplates.Update(existing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var template = await _context.ComplaintTemplates
                .FirstOrDefaultAsync(t => t.Id == id && t.CustomerId == userId);
            if (template == null)
                return NotFound();
            return View(template);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var template = await _context.ComplaintTemplates
                .FirstOrDefaultAsync(t => t.Id == id && t.CustomerId == userId);
            if (template != null)
            {
                _context.ComplaintTemplates.Remove(template);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
