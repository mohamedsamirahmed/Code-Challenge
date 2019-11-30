﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VehicleDashboard.VehicleConnection.Data;

namespace VehiclesDashboard.VehicleConnection.API.Migrations
{
    [DbContext(typeof(VehicleConnectionHistoryDataContext))]
    [Migration("20191130152441_AddCustomerNameColumn")]
    partial class AddCustomerNameColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("VehicleDashboard.VehicleConnection.Domain.Model.CustomerVehicleHistory", b =>
                {
                    b.Property<int>("HistoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("ConnectionStatus");

                    b.Property<int>("CustomerId");

                    b.Property<string>("CustomerName")
                        .IsRequired();

                    b.Property<string>("RegNo")
                        .IsRequired();

                    b.Property<DateTime>("StatusModificationTime");

                    b.Property<string>("VehicleId")
                        .IsRequired();

                    b.HasKey("HistoryId");

                    b.ToTable("CustomerVehicleHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
