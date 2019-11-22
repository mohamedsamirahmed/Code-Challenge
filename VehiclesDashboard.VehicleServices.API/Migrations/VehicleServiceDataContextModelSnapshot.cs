﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VehicleDashboard.VehicleService.Data;

namespace VehiclesDashboard.VehicleServices.API.Migrations
{
    [DbContext(typeof(VehicleServiceDataContext))]
    partial class VehicleServiceDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("VehicleDashboard.VehicleService.Domain.Model.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(100);

                    b.Property<string>("City")
                        .HasMaxLength(25);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("PostalCode");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");

                    b.HasData(
                        new { CustomerId = 1, Address = "Cementvägen 8", City = "Södertälje", Name = "Kalles Grustransporter AB", PostalCode = "111 11" },
                        new { CustomerId = 2, Address = "Balkvägen 12", City = "Stockholm", Name = "Johans Bulk AB", PostalCode = "222 22" },
                        new { CustomerId = 3, Address = "Budgetvägen 1", City = "Uppsala", Name = "Haralds Värdetransporter AB", PostalCode = "333 33" }
                    );
                });

            modelBuilder.Entity("VehicleDashboard.VehicleService.Domain.Model.CustomerVehicle", b =>
                {
                    b.Property<int>("CustomerId");

                    b.Property<string>("VehicleId");

                    b.Property<bool>("IsConnectedStatus");

                    b.Property<DateTime>("LastStatusModificationTime");

                    b.Property<string>("RegNo")
                        .IsRequired();

                    b.HasKey("CustomerId", "VehicleId");

                    b.HasIndex("VehicleId");

                    b.ToTable("CustomerVehicles");

                    b.HasData(
                        new { CustomerId = 1, VehicleId = "YS2R4X20005399401", IsConnectedStatus = true, LastStatusModificationTime = new DateTime(2019, 11, 22, 16, 22, 21, 852, DateTimeKind.Local), RegNo = "ABC123" },
                        new { CustomerId = 1, VehicleId = "VLUR4X20009093588", IsConnectedStatus = true, LastStatusModificationTime = new DateTime(2019, 11, 22, 16, 22, 21, 853, DateTimeKind.Local), RegNo = "DEF456" },
                        new { CustomerId = 1, VehicleId = "VLUR4X20009048066", IsConnectedStatus = true, LastStatusModificationTime = new DateTime(2019, 11, 22, 16, 22, 21, 853, DateTimeKind.Local), RegNo = "GHI789" },
                        new { CustomerId = 2, VehicleId = "YS2R4X20005388011", IsConnectedStatus = true, LastStatusModificationTime = new DateTime(2019, 11, 22, 16, 22, 21, 853, DateTimeKind.Local), RegNo = "JKL012" },
                        new { CustomerId = 2, VehicleId = "YS2R4X20005387949", IsConnectedStatus = true, LastStatusModificationTime = new DateTime(2019, 11, 22, 16, 22, 21, 853, DateTimeKind.Local), RegNo = "MNO345" },
                        new { CustomerId = 3, VehicleId = "VLUR4X20009048066", IsConnectedStatus = true, LastStatusModificationTime = new DateTime(2019, 11, 22, 16, 22, 21, 853, DateTimeKind.Local), RegNo = "PQR678" },
                        new { CustomerId = 3, VehicleId = "YS2R4X20005387055", IsConnectedStatus = true, LastStatusModificationTime = new DateTime(2019, 11, 22, 16, 22, 21, 853, DateTimeKind.Local), RegNo = "STU901" }
                    );
                });

            modelBuilder.Entity("VehicleDashboard.VehicleService.Domain.Model.Vehicle", b =>
                {
                    b.Property<string>("VIN")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("IsDeleted");

                    b.HasKey("VIN");

                    b.ToTable("Vehicle");

                    b.HasData(
                        new { VIN = "YS2R4X20005399401" },
                        new { VIN = "VLUR4X20009093588" },
                        new { VIN = "VLUR4X20009048066" },
                        new { VIN = "YS2R4X20005388011" },
                        new { VIN = "YS2R4X20005387949" },
                        new { VIN = "YS2R4X20005387055" }
                    );
                });

            modelBuilder.Entity("VehicleDashboard.VehicleService.Domain.Model.CustomerVehicle", b =>
                {
                    b.HasOne("VehicleDashboard.VehicleService.Domain.Model.Customer", "Customer")
                        .WithMany("CustomerVehicles")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("VehicleDashboard.VehicleService.Domain.Model.Vehicle", "Vehicle")
                        .WithMany("CustomerVehicles")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
