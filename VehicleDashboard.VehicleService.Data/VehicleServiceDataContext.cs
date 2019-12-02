using Microsoft.EntityFrameworkCore;
using System;
using VehicleDashboard.VehicleService.Domain.Model;

namespace VehicleDashboard.VehicleService.Data
{
    public class VehicleServiceDataContext : DbContext
    {
        public VehicleServiceDataContext(DbContextOptions<VehicleServiceDataContext> options) : base(options)
        { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerVehicle>()
           .HasKey(cv => new { cv.CustomerId, cv.VehicleId });

            modelBuilder.Entity<CustomerVehicle>()
                .HasOne(cv => cv.Customer)
                .WithMany(c => c.CustomerVehicles)
                .HasForeignKey(cv => cv.CustomerId);

            modelBuilder.Entity<CustomerVehicle>()
                .HasOne(cv => cv.Vehicle)
                .WithMany(v => v.CustomerVehicles)
                .HasForeignKey(cv => cv.VehicleId);

            modelBuilder.Entity<Customer>().HasData(
            new { CustomerId = 1, Name = "Kalles Grustransporter AB", Address = "Cementvägen 8", City = "Södertälje", PostalCode = "111 11" },
            new { CustomerId = 2, Name = "Johans Bulk AB", Address = "Balkvägen 12", City = "Stockholm", PostalCode = "222 22" },
            new { CustomerId = 3, Name = "Haralds Värdetransporter AB", Address = "Budgetvägen 1", City = "Uppsala", PostalCode = "333 33" }
            );

            modelBuilder.Entity<Vehicle>().HasData(
            new { VIN = "YS2R4X20005399401" },
            new { VIN = "VLUR4X20009093588" },
            new { VIN = "VLUR4X20009048066" },
            new { VIN = "YS2R4X20005388011" },
            new { VIN = "YS2R4X20005387949" },
            new { VIN = "YS2R4X20005387055" }
            );

            modelBuilder.Entity<CustomerVehicle>().HasData(
                new { CustomerId = 1, VehicleId = "YS2R4X20005399401", RegNo = "ABC123", IsConnectedStatus = true, LastStatusModificationTime = DateTime.Now },
                new { CustomerId = 1, VehicleId = "VLUR4X20009093588", RegNo = "DEF456", IsConnectedStatus = true, LastStatusModificationTime = DateTime.Now },
                new { CustomerId = 1, VehicleId = "VLUR4X20009048066", RegNo = "GHI789", IsConnectedStatus = true, LastStatusModificationTime = DateTime.Now },
                new { CustomerId = 2, VehicleId = "YS2R4X20005388011", RegNo = "JKL012", IsConnectedStatus = true, LastStatusModificationTime = DateTime.Now },
                new { CustomerId = 2, VehicleId = "YS2R4X20005387949", RegNo = "MNO345", IsConnectedStatus = true, LastStatusModificationTime = DateTime.Now },
                new { CustomerId = 3, VehicleId = "VLUR4X20009048066", RegNo = "PQR678", IsConnectedStatus = true, LastStatusModificationTime = DateTime.Now },
                new { CustomerId = 3, VehicleId = "YS2R4X20005387055", RegNo = "STU901", IsConnectedStatus = true, LastStatusModificationTime = DateTime.Now }
                );

        }

    }
}
