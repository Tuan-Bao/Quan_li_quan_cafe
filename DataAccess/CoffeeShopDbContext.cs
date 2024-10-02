using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quan_li_quan_cafe.Models;

namespace Quan_li_quan_cafe.DataAccess
{
    public class CoffeeShopDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Coffee> Coffees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;database=coffeeshop;user=root;password=12345",
                new MySqlServerVersion(new Version(8, 0, 21)));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình mối quan hệ giữa User và Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            // Cấu hình mối quan hệ giữa Order và OrderItem
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cấu hình mối quan hệ giữa OrderItem và Coffee
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Coffee)
                .WithMany(c => c.OrderItems)
                .HasForeignKey(oi => oi.CoffeeId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
