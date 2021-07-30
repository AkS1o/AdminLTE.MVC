using System;
using System.Collections.Generic;
using System.Text;
using AdminLTE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdminLTE.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            this.SeedUsers(builder);
            this.SeedRoles(builder);
            this.SeedUserRoles(builder);
        }

        private void SeedUsers(ModelBuilder builder)
        {
            PasswordHasher<IdentityUser> passwordHash = new PasswordHasher<IdentityUser>();

            var identityUser = new IdentityUser()
            {
                Id = "d7b98713-435b-42f1-917f-45f226586b92",
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                LockoutEnabled = false,
                PasswordHash = passwordHash.HashPassword(null, "Admin!23")
            };

            builder.Entity<IdentityUser>().HasData(identityUser);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            IdentityRole identityRole = new IdentityRole()
            {
                Id = "fab4fac1-c546-41de-aebc-a14da6895711",
                Name = "Admin",
                ConcurrencyStamp = "1",
                NormalizedName = "Admin"
            };

            builder.Entity<IdentityRole>().HasData(identityRole);
        }

        private void SeedUserRoles(ModelBuilder builder)
        {
            IdentityUserRole<string> identityUserRole = new IdentityUserRole<string>
            {
                RoleId = "fab4fac1-c546-41de-aebc-a14da6895711",
                UserId = "d7b98713-435b-42f1-917f-45f226586b92"
            };

            builder.Entity<IdentityUserRole<string>>().HasData(identityUserRole);
        }

        public DbSet<Community> Communities { get; set; }
        public DbSet<Worker> Workers { get; set; }
    }
}
