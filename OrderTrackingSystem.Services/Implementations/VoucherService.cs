using Microsoft.EntityFrameworkCore;
using OrderTrackingSystem.Data;
using OrderTrackingSystem.Domain.Models;
using OrderTrackingSystem.Services.Interfaces;

namespace OrderTrackingSystem.Services.Implementations
{
    public class VoucherService : IVoucherService
    {
        private readonly ApplicationDbContext _context;
        public VoucherService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Voucher>> GetAllVouchersAsync() =>
            await _context.Vouchers.ToListAsync();
        public async Task<Voucher> GetVoucherByIdAsync(int id) =>
            await _context.Vouchers.FirstOrDefaultAsync(v => v.Id == id);
        public async Task CreateVoucherAsync(Voucher voucher)
        {
            _context.Vouchers.Add(voucher);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateVoucherAsync(Voucher voucher)
        {
            _context.Vouchers.Update(voucher);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteVoucherAsync(int id)
        {
            var voucher = await GetVoucherByIdAsync(id);
            if (voucher != null)
            {
                _context.Vouchers.Remove(voucher);
                await _context.SaveChangesAsync();
            }
        }
    }
}
