﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using print_quotes_calculator.Utilities;

#nullable disable

namespace print_quotes_calculator.Migrations
{
    [DbContext(typeof(QuoteContext))]
    [Migration("20241021153154_QuoteDatabase")]
    partial class QuoteDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("print_quotes_calculator.Utilities.Ink", b =>
                {
                    b.Property<long>("InkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Cost")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("InkId");

                    b.ToTable("Inks");
                });

            modelBuilder.Entity("print_quotes_calculator.Utilities.Material", b =>
                {
                    b.Property<long>("MaterialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Cost")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MaterialId");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("print_quotes_calculator.Utilities.Quote", b =>
                {
                    b.Property<long>("QuoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Ink")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("InkUsage")
                        .HasColumnType("TEXT");

                    b.Property<string>("Material")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("MaterialUsage")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("QuoteCost")
                        .HasColumnType("TEXT");

                    b.HasKey("QuoteId");

                    b.ToTable("Quotes");
                });
#pragma warning restore 612, 618
        }
    }
}
