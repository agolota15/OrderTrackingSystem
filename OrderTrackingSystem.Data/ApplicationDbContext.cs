<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
=======
﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
using Microsoft.EntityFrameworkCore;
using OrderTrackingSystem.Domain.Models;

namespace OrderTrackingSystem.Data
{
<<<<<<< HEAD
    // Dziedziczymy po IdentityDbContext, aby dodać tabele Identity (AspNetUsers, AspNetRoles itd.)
    public class ApplicationDbContext : IdentityDbContext
=======
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
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

<<<<<<< HEAD
            // Konfiguracja relacji Order - OrderHistory
=======
            // Konfiguracja relacji: Order – OrderHistory
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
            modelBuilder.Entity<OrderHistory>()
                .HasOne(oh => oh.Order)
                .WithMany(o => o.Histories)
                .HasForeignKey(oh => oh.OrderId);

<<<<<<< HEAD
            // Konfiguracja relacji Order - OrderItem
=======
            // Konfiguracja relacji: Order – OrderItem
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);
        }
    }
}
