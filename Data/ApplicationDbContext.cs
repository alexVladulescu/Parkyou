using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Parkyou.Models;

namespace Parkyou.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Shorten key length for Identity
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(m => m.Email).HasMaxLength(127);
                entity.Property(m => m.NormalizedEmail).HasMaxLength(127);
                entity.Property(m => m.NormalizedUserName).HasMaxLength(127);
                entity.Property(m => m.UserName).HasMaxLength(127);
                entity.Property(m => m.Id).ValueGeneratedOnAdd().HasMaxLength(36);
            });
            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.Property(m => m.Name).HasMaxLength(127);
                entity.Property(m => m.NormalizedName).HasMaxLength(127);
            });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.Property(m => m.LoginProvider).HasMaxLength(127);
                entity.Property(m => m.ProviderKey).HasMaxLength(127);
            });
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.Property(m => m.UserId).HasMaxLength(127);
                entity.Property(m => m.RoleId).HasMaxLength(127);
            });
            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.Property(m => m.UserId).HasMaxLength(127);
                entity.Property(m => m.LoginProvider).HasMaxLength(127);
                entity.Property(m => m.Name).HasMaxLength(127);
            });
            modelBuilder.Entity<ParkingSpot>(entity =>
            {
                entity.Property(m => m.Id).ValueGeneratedOnAdd().HasMaxLength(127);
            });
            modelBuilder.Entity<Report>(entity =>
            {
                entity.Property(m => m.Id).ValueGeneratedOnAdd().HasMaxLength(36);
                entity.Property(m => m.Title).HasMaxLength(100);
                entity.Property(m => m.Description).HasMaxLength(500);
                entity.Property(m => m.Resolution).HasMaxLength(500);
            });

            Administrator admin = new Administrator()
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "vladulescualexandru1@gmail.com",
                NormalizedEmail = "VLADULESCUALEXANDRU1@GMAIL.COM",
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794577",
                EmailConfirmed = true,
                LockoutEnabled = true,
                FirstName = "Admin",
                LastName = "Admin",
                Rol = "Administrator"
            };
            var passwordHash = new PasswordHasher<Administrator>();
            admin.PasswordHash = passwordHash.HashPassword(admin, "Welcome@2021");
            modelBuilder.Entity<Administrator>().HasData(admin);

            int id = 1;
            foreach (char row in "ABCDEFGHI".ToCharArray())
            {
                for (int col = 1; col <= 10; col++)
                {
                    ParkingSpot ps = new ParkingSpot()
                    {
                        Row = row.ToString(),
                        Col = col,
                        Id = id++,
                        Status = 0,
                        UserName = ""
                    };
                    modelBuilder.Entity<ParkingSpot>().HasData(ps);
                }
            }
        }

        public DbSet<User> AppUsers { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<ParkingSpot> ParkingSpots { get; set; }
        public DbSet<Report> Reports { get; set; }
    }
}