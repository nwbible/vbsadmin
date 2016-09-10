﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VBSAdmin.Models;
using VBSAdmin.Models.VBSAdminModels;

namespace VBSAdmin.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<VBS> VBS { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Guardian> Guardians { get; set; }
        public DbSet<Child> Children { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<Child>()
                .HasOne(c => c.VBS)
                .WithMany(v => v.Children)
                .IsRequired()
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);

            builder.Entity<Child>()
                .HasOne(c => c.Session)
                .WithMany(s => s.Children)
                .IsRequired()
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);

        }
    }
}
