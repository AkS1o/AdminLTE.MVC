using System;
using System.Collections.Generic;
using System.Text;
using AdminLTE.Models;
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

        public DbSet<Community> Communities { get; set; }
        public DbSet<Worker> Workers { get; set; }
    }
}
