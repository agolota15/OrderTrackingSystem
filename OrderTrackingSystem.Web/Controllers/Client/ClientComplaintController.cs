using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderTrackingSystem.Data;
using OrderTrackingSystem.Domain.Models;
using System.Security.Claims;

namespace OrderTrackingSystem.Web.Controllers.Client
{
    [Authorize(Roles = "Customer")]
    public class ClientComplaintController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientComplaintController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /ClientComplaint
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var complaints = await _context.Complaints
                .Where(c => c.CustomerId == userId)
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();
            return View(complaints);
        }

        // GET: /ClientComplaint/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var complaint = await _context.Complaints
                .FirstOrDefaultAsync(c => c.Id == id && c.CustomerId == userId);
            if (complaint == null)
                return NotFound();
            return View(complaint);
        }

        // GET: /ClientComplaint/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /ClientComplaint/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Complaint complaint)
        {
            if (!ModelState.IsValid)
                return View(complaint);

            complaint.CustomerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            complaint.CreatedDate = DateTime.Now;
            complaint.Status = "Pending";
            _context.Complaints.Add(complaint);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /ClientComplaint/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var complaint = await _context.Complaints
                .FirstOrDefaultAsync(c => c.Id == id && c.CustomerId == userId);
            if (complaint == null)
                return NotFound();

            return View(complaint);
        }

        // POST: /ClientComplaint/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Complaint complaint)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id != complaint.Id)
                return NotFound();

            // Upewnij się, że należy do zalogowanego użytkownika
            var existing = await _context.Complaints
                .FirstOrDefaultAsync(c => c.Id == id && c.CustomerId == userId);
            if (existing == null)
                return NotFound();

            if (!ModelState.IsValid)
                return View(complaint);

            existing.Subject = complaint.Subject;
            existing.Description = complaint.Description;
            // Status może być edytowany przez klienta tylko w ograniczonym zakresie (lub wcale)
            // existing.Status = complaint.Status;

            _context.Complaints.Update(existing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /ClientComplaint/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var complaint = await _context.Complaints
                .FirstOrDefaultAsync(c => c.Id == id && c.CustomerId == userId);
            if (complaint == null)
                return NotFound();

            return View(complaint);
        }

        // POST: /ClientComplaint/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var complaint = await _context.Complaints
                .FirstOrDefaultAsync(c => c.Id == id && c.CustomerId == userId);
            if (complaint != null)
            {
                _context.Complaints.Remove(complaint);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
