using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderTrackingSystem.Data;
using OrderTrackingSystem.Domain.Models;

namespace OrderTrackingSystem.Web.Controllers.Seller
{
    [Authorize(Roles = "Seller")]
    public class SellerComplaintController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SellerComplaintController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /SellerComplaint
        public async Task<IActionResult> Index()
        {
            var complaints = await _context.Complaints
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();
            return View(complaints);
        }

        // GET: /SellerComplaint/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint == null)
                return NotFound();
            return View(complaint);
        }

        // Sprzedawca raczej nie tworzy reklamacji, ale można dodać Create:
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Complaint complaint)
        {
            if (!ModelState.IsValid)
                return View(complaint);

            complaint.CreatedDate = DateTime.Now;
            _context.Complaints.Add(complaint);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /SellerComplaint/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint == null)
                return NotFound();
            return View(complaint);
        }

        // POST: /SellerComplaint/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Complaint complaint)
        {
            if (id != complaint.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(complaint);

            var existing = await _context.Complaints.FindAsync(id);
            if (existing == null)
                return NotFound();

            existing.Subject = complaint.Subject;
            existing.Description = complaint.Description;
            existing.Status = complaint.Status;
            // itp.

            _context.Complaints.Update(existing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /SellerComplaint/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint == null)
                return NotFound();
            return View(complaint);
        }

        // POST: /SellerComplaint/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint != null)
            {
                _context.Complaints.Remove(complaint);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
