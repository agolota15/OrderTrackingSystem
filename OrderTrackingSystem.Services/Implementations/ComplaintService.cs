using Microsoft.EntityFrameworkCore;
using OrderTrackingSystem.Data;
using OrderTrackingSystem.Domain.Models;
using OrderTrackingSystem.Services.Interfaces;

namespace OrderTrackingSystem.Services.Implementations
{
    public class ComplaintService : IComplaintService
    {
        private readonly ApplicationDbContext _context;
        public ComplaintService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Complaint>> GetAllComplaintsAsync() =>
            await _context.Complaints.ToListAsync();
        public async Task<Complaint> GetComplaintByIdAsync(int id) =>
            await _context.Complaints.FirstOrDefaultAsync(c => c.Id == id);
        public async Task CreateComplaintAsync(Complaint complaint)
        {
            _context.Complaints.Add(complaint);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateComplaintAsync(Complaint complaint)
        {
            _context.Complaints.Update(complaint);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteComplaintAsync(int id)
        {
            var complaint = await GetComplaintByIdAsync(id);
            if (complaint != null)
            {
                _context.Complaints.Remove(complaint);
                await _context.SaveChangesAsync();
            }
        }
    }
}
