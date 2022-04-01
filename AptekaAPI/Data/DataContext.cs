using AptekaAPI.Models;
using AptekaAPI.Models.Admin.PostModel;
using AptekaAPI.Models.Medicine;
using AptekaAPI.Models.Vendor.PostModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AptekaAPI.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {}

        public DbSet<Vendor> Users { get; set; }
        public DbSet<Country> Countrys { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<MedicineType> MedicineTypes { get; set; }
        //public DbSet<Sales> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vendor>()
                .HasIndex(x => x.Username)
                .IsUnique(true);
            modelBuilder.Entity<Vendor>()
                .HasIndex(x => x.PhoneNumber)
                .IsUnique(true);
            modelBuilder.Entity<Country>()
                .HasIndex(x => x.Name)
                .IsUnique(true);
            modelBuilder.Entity<SellMedicine>(
            eb =>
            {
                eb.HasNoKey();
            });
        }
    }
}
