using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderTrackingSystem.Data;
using OrderTrackingSystem.Domain.Models;
using System.Security.Claims;

namespace OrderTrackingSystem.Web.Controllers.Client
{
    [Authorize(Roles = "Customer")]
    public class ClientVoucherController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientVoucherController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /ClientVoucher
        public async Task<IActionResult> Index()
        {
            // Można filtrować, jeśli vouchery są przypisane do usera
            var vouchers = await _context.Vouchers
                .Where(v => !v.IsUsed && v.ExpirationDate > DateTime.Now)
                .OrderBy(v => v.ExpirationDate)
                .ToListAsync();
            return View(vouchers);
        }

        // GET: /ClientVoucher/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var voucher = await _context.Vouchers.FindAsync(id);
            if (voucher == null) return NotFound();
            return View(voucher);
        }

        // Klient raczej nie tworzy voucherów, więc Create/Edit/Delete są zbędne
        // Chyba że chcesz pozwolić klientowi "usuwać" niewykorzystane bony?

        // Przykład "wykorzystania" vouchera:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApplyVoucher(int voucherId)
        {
            var voucher = await _context.Vouchers.FindAsync(voucherId);
            if (voucher == null || voucher.IsUsed || voucher.ExpirationDate < DateTime.Now)
                return BadRequest("Voucher invalid or expired.");

            // Logika naliczania rabatu, np. do zamówienia
            // ...

            voucher.IsUsed = true;
            _context.Vouchers.Update(voucher);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
