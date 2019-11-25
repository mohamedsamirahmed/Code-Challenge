using Microsoft.EntityFrameworkCore;
using System;
using VehicleDashboard.VehicleConnection.Domain.Model;

namespace VehicleDashboard.VehicleConnection.Data
{
    public class VehicleConnectionHistoryDataContext : DbContext
    {
        public VehicleConnectionHistoryDataContext(DbContextOptions<VehicleConnectionHistoryDataContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerVehicleHistory>().Property(p => p.HistoryId).ValueGeneratedOnAdd();
        }

        public DbSet<CustomerVehicleHistory> Customers { get; set; }
       
    }
}
