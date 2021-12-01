using AlifAvitoProj.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlifAvitoProj.Context
{
    public class AvitoDbContext : DbContext
    {
        public AvitoDbContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }

        public virtual DbSet<Advert> Adverts { get; set; } 
        public virtual DbSet<User> Users { get; set; } 
        public virtual DbSet<Category> Categories { get; set; } 
        public virtual DbSet<City> Cities { get; set; } 
        public virtual DbSet<Review> Reviews { get; set; } 
        public virtual DbSet<Reviewer> Reviewers { get; set; } 
        public virtual DbSet<AdvertUser> AdvertUsers { get; set; } 
        public virtual DbSet<AdvertCategory> AdvertCategories { get; set; }
        public virtual DbSet<WarningUser> WarningUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdvertCategory>()
                        .HasKey(bc => new { bc.AdvertId, bc.CategoryId });
            modelBuilder.Entity<AdvertCategory>()
                        .HasOne(b => b.Advert)
                        .WithMany(bc => bc.AdvertCategories)
                        .HasForeignKey(b => b.AdvertId);
            modelBuilder.Entity<AdvertCategory>()
                        .HasOne(c => c.Category)
                        .WithMany(bc => bc.AdvertCategories)
                        .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<AdvertUser>()
                        .HasKey(ba => new { ba.AdvertId, ba.UserId });
            modelBuilder.Entity<AdvertUser>()
                        .HasOne(b => b.Advert)
                        .WithMany(ba => ba.AdvertUsers)
                        .HasForeignKey(b => b.AdvertId);
            modelBuilder.Entity<AdvertUser>()
                        .HasOne(a => a.User)
                        .WithMany(ba => ba.AdvertUsers)
                        .HasForeignKey(a => a.UserId);
        }
    }
}
