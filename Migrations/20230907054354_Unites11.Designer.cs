﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShopSystem.Context;

#nullable disable

namespace ShopSystem.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230907054354_Unites11")]
    partial class Unites11
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ShopSystem.Models.AddedProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("AddedCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("Added_time")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<double>("NewPriceCome")
                        .HasColumnType("double");

                    b.Property<double>("NewPriceGo")
                        .HasColumnType("double");

                    b.Property<double>("PriceCome")
                        .HasColumnType("double");

                    b.Property<double>("PriceGo")
                        .HasColumnType("double");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("AdditionalProducts");
                });

            modelBuilder.Entity("ShopSystem.Models.Cashbox", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CahsregisterId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("ShopId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Кассы");
                });

            modelBuilder.Entity("ShopSystem.Models.CashedProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Barcode")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CashedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("Categoryid")
                        .HasColumnType("char(36)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created_time")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("PriceCome")
                        .HasColumnType("double");

                    b.Property<double>("PriceGo")
                        .HasColumnType("double");

                    b.Property<Guid?>("ShopId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Totalcount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CashedProducts");
                });

            modelBuilder.Entity("ShopSystem.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created_time")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("ShopId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ShopSystem.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Barcode")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("Categoryid")
                        .HasColumnType("char(36)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created_time")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("PriceCome")
                        .HasColumnType("double");

                    b.Property<double>("PriceGo")
                        .HasColumnType("double");

                    b.Property<Guid?>("ShopId")
                        .HasColumnType("char(36)");

                    b.Property<string>("TabName")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ShopSystem.Models.Shop", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created_time")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Owner")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("ShopSystem.Models.Subcategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created_time")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ShopId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Subcategories");
                });

            modelBuilder.Entity("ShopSystem.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool?>("RememberMe")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
