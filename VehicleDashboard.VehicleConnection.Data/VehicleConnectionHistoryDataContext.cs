using Microsoft.EntityFrameworkCore;
using System;
using VehicleDashboard.VehicleConnection.Domain.Model;

namespace VehicleDashboard.VehicleConnection.Data
{
    public class VehicleConnectionHistoryDataContext : DbContext
    {
        public VehicleConnectionHistoryDataContext(DbContextOptions<VehicleConnectionHistoryDataContext> options) : base(options)
        { }


        protected VehicleConnectionHistoryDataContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerVehicleHistory>().Property(p => p.HistoryId).ValueGeneratedOnAdd();

            var properties = typeof(CustomerVehicleHistory).GetProperties();

            foreach (var prop in properties)
            {
                modelBuilder.Entity<CustomerVehicleHistory>().Property(prop.PropertyType, prop.Name).IsRequired();
            }

        }

        public  DbSet<CustomerVehicleHistory> CustomerehicleHistory { get; set; }
       
    }
}
