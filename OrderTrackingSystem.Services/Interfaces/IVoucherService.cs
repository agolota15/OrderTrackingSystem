using OrderTrackingSystem.Domain.Models;

namespace OrderTrackingSystem.Services.Interfaces
{
    public interface IVoucherService
    {
        Task<List<Voucher>> GetAllVouchersAsync();
        Task<Voucher> GetVoucherByIdAsync(int id);
        Task CreateVoucherAsync(Voucher voucher);
        Task UpdateVoucherAsync(Voucher voucher);
        Task DeleteVoucherAsync(int id);
    }
}
