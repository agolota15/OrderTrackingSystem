using OrderTrackingSystem.Domain.Models;

namespace OrderTrackingSystem.Services.Interfaces
{
    public interface IComplaintService
    {
        Task<List<Complaint>> GetAllComplaintsAsync();
        Task<Complaint> GetComplaintByIdAsync(int id);
        Task CreateComplaintAsync(Complaint complaint);
        Task UpdateComplaintAsync(Complaint complaint);
        Task DeleteComplaintAsync(int id);
    }
}
