﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MonitoringStationAPI.Database;

#nullable disable

namespace MonitoringStationAPI.Migrations
{
    [DbContext(typeof(EnvironmentalDataContext))]
    partial class EnvironmentalDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("MonitoringStationAPI.Models.Sensor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DataCollectionInterval")
                        .HasColumnType("TEXT");

                    b.Property<float>("DataRangeMax")
                        .HasColumnType("REAL");

                    b.Property<float>("DataRangeMin")
                        .HasColumnType("REAL");

                    b.Property<double?>("Humidity")
                        .HasColumnType("REAL");

                    b.Property<float?>("NormalThresholdMax")
                        .HasColumnType("REAL");

                    b.Property<float?>("NormalThresholdMin")
                        .HasColumnType("REAL");

                    b.Property<string>("Parameter")
                        .HasColumnType("TEXT");

                    b.Property<double>("ParameterValue")
                        .HasColumnType("REAL");

                    b.Property<double?>("Rainfall")
                        .HasColumnType("REAL");

                    b.Property<int>("SensorId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Unit")
                        .HasColumnType("TEXT");

                    b.Property<string>("Warning")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Sensor");
                });
#pragma warning restore 612, 618
        }
    }
}
