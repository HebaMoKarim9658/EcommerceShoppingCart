using AngularProjectAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProjectAPI.Data
{
    public class ApplicationDbContext: IdentityDbContext<User, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OrderDetails>().HasKey(l => new { l.OrderID, l.ProductID });

            builder.Entity<IdentityRole>().HasData(
                new { Id = "980aba08-99d2-4e11-9115-c97c088ba7e6", Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "82d95fa3-1a8e-4c1d-bf97-d10ae8a028c1" },
                new { Id = "40b3e90f-d111-4f6a-af4a-26059c16a75f", Name = "Normal User", NormalizedName = "NORMAL USER", ConcurrencyStamp= "b1445195-db44-4c08-90c7-d0ae3898a03e" }
                );
                

            base.OnModelCreating(builder);
        }
    }
}
