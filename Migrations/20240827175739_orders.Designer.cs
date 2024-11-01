﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using e_commerce_api.Data;

#nullable disable

namespace e_commerce_api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240827175739_orders")]
    partial class orders
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("e_commerce_api.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("e_commerce_api.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Latest Samsung flagship smartphone with 8GB RAM and 128GB storage.",
                            Name = "Samsung Galaxy S21",
                            Price = 799.99m,
                            StockQuantity = 50
                        },
                        new
                        {
                            Id = 2,
                            Description = "Apple MacBook Pro with M1 chip, 16GB RAM, and 512GB SSD.",
                            Name = "Apple MacBook Pro",
                            Price = 1299.99m,
                            StockQuantity = 30
                        },
                        new
                        {
                            Id = 3,
                            Description = "Noise-cancelling wireless headphones with 30-hour battery life.",
                            Name = "Sony WH-1000XM4",
                            Price = 349.99m,
                            StockQuantity = 100
                        },
                        new
                        {
                            Id = 4,
                            Description = "Dell XPS 13 laptop with 11th Gen Intel Core i7, 16GB RAM, and 1TB SSD.",
                            Name = "Dell XPS 13",
                            Price = 1499.99m,
                            StockQuantity = 20
                        },
                        new
                        {
                            Id = 5,
                            Description = "GoPro HERO9 action camera with 5K video and 20MP photos.",
                            Name = "GoPro HERO9",
                            Price = 399.99m,
                            StockQuantity = 75
                        },
                        new
                        {
                            Id = 6,
                            Description = "Nintendo Switch console with Neon Blue and Neon Red Joy‑Con.",
                            Name = "Nintendo Switch",
                            Price = 299.99m,
                            StockQuantity = 150
                        },
                        new
                        {
                            Id = 7,
                            Description = "Apple AirPods Pro with Active Noise Cancellation and Wireless Charging Case.",
                            Name = "Apple AirPods Pro",
                            Price = 249.99m,
                            StockQuantity = 200
                        },
                        new
                        {
                            Id = 8,
                            Description = "Canon EOS R6 full-frame mirrorless camera with 4K video and 20.1MP sensor.",
                            Name = "Canon EOS R6",
                            Price = 2499.99m,
                            StockQuantity = 10
                        },
                        new
                        {
                            Id = 9,
                            Description = "Bose SoundLink Revolve portable Bluetooth speaker with 360-degree sound.",
                            Name = "Bose SoundLink Revolve",
                            Price = 199.99m,
                            StockQuantity = 120
                        },
                        new
                        {
                            Id = 10,
                            Description = "Fitbit Charge 4 fitness and activity tracker with built-in GPS.",
                            Name = "Fitbit Charge 4",
                            Price = 149.99m,
                            StockQuantity = 250
                        });
                });

            modelBuilder.Entity("e_commerce_api.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("e_commerce_api.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("e_commerce_api.Models.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId", "RoleId")
                        .IsUnique();

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("OrderItem", b =>
                {
                    b.HasOne("e_commerce_api.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("e_commerce_api.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("e_commerce_api.Models.Order", b =>
                {
                    b.HasOne("e_commerce_api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("e_commerce_api.Models.UserRole", b =>
                {
                    b.HasOne("e_commerce_api.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("e_commerce_api.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("e_commerce_api.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("e_commerce_api.Models.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("e_commerce_api.Models.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
