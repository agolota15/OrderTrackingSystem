using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrderTrackingSystem.Domain.Models;

namespace OrderTrackingSystem.Data
{
    // Dziedziczymy po IdentityDbContext, aby dodać tabele Identity (AspNetUsers, AspNetRoles itd.)
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderHistory> OrderHistories { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfiguracja relacji Order - OrderHistory
            modelBuilder.Entity<OrderHistory>()
                .HasOne(oh => oh.Order)
                .WithMany(o => o.Histories)
                .HasForeignKey(oh => oh.OrderId);

            // Konfiguracja relacji Order - OrderItem
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);
        }
    }
}
