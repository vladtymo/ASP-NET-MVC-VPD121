﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shop_MVC_VPD_121.Data;

#nullable disable

namespace Shop_MVC_VPD_121.Migrations
{
    [DbContext(typeof(ShopDbContext))]
    [Migration("20230326115842_SeedData")]
    partial class SeedData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Shop_MVC_VPD_121.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Electronics"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Sport"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Fashion"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Home & Garden"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Transport"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Toys & Hobbies"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Musical Instruments"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Art"
                        });
                });

            modelBuilder.Entity("Shop_MVC_VPD_121.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Name = "iPhone X",
                            Price = 650m
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            Name = "PowerBall",
                            Price = 45.5m
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 3,
                            Name = "Nike T-Shirt",
                            Price = 189m
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 1,
                            Name = "Samsung S23",
                            Price = 1200m
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 6,
                            Name = "Air Ball",
                            Price = 50m
                        },
                        new
                        {
                            Id = 6,
                            CategoryId = 1,
                            Name = "MacBook Pro 2019",
                            Price = 1200m
                        });
                });

            modelBuilder.Entity("Shop_MVC_VPD_121.Entities.Product", b =>
                {
                    b.HasOne("Shop_MVC_VPD_121.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Shop_MVC_VPD_121.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
