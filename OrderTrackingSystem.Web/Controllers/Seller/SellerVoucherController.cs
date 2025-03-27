using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderTrackingSystem.Data;
using OrderTrackingSystem.Domain.Models;

namespace OrderTrackingSystem.Web.Controllers.Seller
{
    [Authorize(Roles = "Seller")]
    public class SellerVoucherController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SellerVoucherController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /SellerVoucher
        public async Task<IActionResult> Index()
        {
            var vouchers = await _context.Vouchers
                .OrderBy(v => v.ExpirationDate)
                .ToListAsync();
            return View(vouchers);
        }

        // GET: /SellerVoucher/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var voucher = await _context.Vouchers.FindAsync(id);
            if (voucher == null)
                return NotFound();
            return View(voucher);
        }

        // GET: /SellerVoucher/Create
        public IActionResult Create() => View();

        // POST: /SellerVoucher/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Voucher voucher)
        {
            if (!ModelState.IsValid)
                return View(voucher);

            _context.Vouchers.Add(voucher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /SellerVoucher/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var voucher = await _context.Vouchers.FindAsync(id);
            if (voucher == null)
                return NotFound();
            return View(voucher);
        }

        // POST: /SellerVoucher/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Voucher updated)
        {
            if (id != updated.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(updated);

            var voucher = await _context.Vouchers.FindAsync(id);
            if (voucher == null)
                return NotFound();

            voucher.Code = updated.Code;
            voucher.DiscountValue = updated.DiscountValue;
            voucher.ExpirationDate = updated.ExpirationDate;
            voucher.IsUsed = updated.IsUsed;

            _context.Vouchers.Update(voucher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /SellerVoucher/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var voucher = await _context.Vouchers.FindAsync(id);
            if (voucher == null)
                return NotFound();
            return View(voucher);
        }

        // POST: /SellerVoucher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var voucher = await _context.Vouchers.FindAsync(id);
            if (voucher != null)
            {
                _context.Vouchers.Remove(voucher);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
