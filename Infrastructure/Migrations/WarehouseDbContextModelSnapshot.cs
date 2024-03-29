﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(WarehouseDbContext))]
    partial class WarehouseDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Entities.Movement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("StockItemId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StockItemId");

                    b.ToTable("Movements");
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Domain.Entities.OrderItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("OrderId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("ABCRating")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(2);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("SpaceRequirements")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Domain.Entities.Stock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("OccupiendCapacity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Stocks", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.StockItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("BarCode")
                        .HasColumnType("longtext");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("StockId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("StockId");

                    b.ToTable("StockItems");
                });

            modelBuilder.Entity("Domain.Entities.Movement", b =>
                {
                    b.HasOne("Domain.Entities.StockItem", "StockItem")
                        .WithMany("Movements")
                        .HasForeignKey("StockItemId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("StockItem");
                });

            modelBuilder.Entity("Domain.Entities.OrderItem", b =>
                {
                    b.HasOne("Domain.Entities.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.OwnsOne("Domain.Entities.Money", "Price", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("char(36)");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(65,30)");

                            b1.Property<int>("Currency")
                                .HasColumnType("int");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsOne("Domain.Entities.Size", "PackageSize", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("char(36)");

                            b1.Property<int>("Depth")
                                .HasColumnType("int");

                            b1.Property<int>("Height")
                                .HasColumnType("int");

                            b1.Property<int>("Width")
                                .HasColumnType("int");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.Navigation("PackageSize")
                        .IsRequired();

                    b.Navigation("Price")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Stock", b =>
                {
                    b.OwnsOne("Domain.Entities.Position", "Position", b1 =>
                        {
                            b1.Property<Guid>("StockId")
                                .HasColumnType("char(36)");

                            b1.Property<int>("X")
                                .HasColumnType("int");

                            b1.Property<int>("Y")
                                .HasColumnType("int");

                            b1.HasKey("StockId");

                            b1.ToTable("Stocks");

                            b1.WithOwner()
                                .HasForeignKey("StockId");
                        });

                    b.Navigation("Position")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.StockItem", b =>
                {
                    b.HasOne("Domain.Entities.Product", "Product")
                        .WithMany("StockItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Stock", "Stock")
                        .WithMany("StockItems")
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.Navigation("StockItems");
                });

            modelBuilder.Entity("Domain.Entities.Stock", b =>
                {
                    b.Navigation("StockItems");
                });

            modelBuilder.Entity("Domain.Entities.StockItem", b =>
                {
                    b.Navigation("Movements");
                });
#pragma warning restore 612, 618
        }
    }
}
