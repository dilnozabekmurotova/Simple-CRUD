using Finance_task2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finance_task2.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Region> Regions { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasOne<Region>(s => s.Region).WithMany(g => g.Users).HasForeignKey(s => s.RegionId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Users>().HasOne<District>(s => s.District).WithMany(g => g.Users).HasForeignKey(s => s.DistrictId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<District>().HasOne<Region>(s => s.region).WithMany(g => g.District).HasForeignKey(s => s.RegionId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
