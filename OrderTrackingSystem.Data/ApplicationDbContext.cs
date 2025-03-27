using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrderTrackingSystem.Domain.Models;

namespace OrderTrackingSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ComplaintTemplate> ComplaintTemplates { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
    }
}
