﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace OnlineMenu.Migrations.TenantAdminDb
{
    [DbContext(typeof(TenantAdminDbContext))]
    partial class TenantAdminDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("OnlineMenu.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("OnlineMenu.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<int?>("TableId")
                        .HasColumnType("int");

                    b.Property<string>("Telephone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.Property<bool>("isClosed")
                        .HasColumnType("bit");

                    b.Property<bool>("isLocal")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("TableId");

                    b.HasIndex("TenantId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("OnlineMenu.Models.Product", b =>
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

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int?>("TenantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("TenantId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("OnlineMenu.Models.ProductOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("TableId")
                        .HasColumnType("int");

                    b.Property<int?>("TenantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.HasIndex("TableId");

                    b.HasIndex("TenantId");

                    b.ToTable("ProductOrder");
                });

            modelBuilder.Entity("OnlineMenu.Models.Table", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("Table");
                });

            modelBuilder.Entity("OnlineMenu.Models.Tenant", b =>
                {
                    b.Property<int>("TenantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TenantId"), 1L, 1);

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TenantId");

                    b.ToTable("Tenants");
                });

            modelBuilder.Entity("OnlineMenu.Models.Category", b =>
                {
                    b.HasOne("OnlineMenu.Models.Tenant", "Tenant")
                        .WithMany("Categories")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("OnlineMenu.Models.Order", b =>
                {
                    b.HasOne("OnlineMenu.Models.Table", "Table")
                        .WithMany("Orders")
                        .HasForeignKey("TableId");

                    b.HasOne("OnlineMenu.Models.Tenant", "Tenant")
                        .WithMany("Orders")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Table");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("OnlineMenu.Models.Product", b =>
                {
                    b.HasOne("OnlineMenu.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineMenu.Models.Tenant", "Tenant")
                        .WithMany("Products")
                        .HasForeignKey("TenantId");

                    b.Navigation("Category");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("OnlineMenu.Models.ProductOrder", b =>
                {
                    b.HasOne("OnlineMenu.Models.Order", "Order")
                        .WithMany("ProductOrders")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineMenu.Models.Product", "Product")
                        .WithMany("ProductOrders")
                        .HasForeignKey("ProductId");

                    b.HasOne("OnlineMenu.Models.Table", null)
                        .WithMany("ProductOrders")
                        .HasForeignKey("TableId");

                    b.HasOne("OnlineMenu.Models.Tenant", "Tenant")
                        .WithMany("ProductOrders")
                        .HasForeignKey("TenantId");

                    b.Navigation("Order");

                    b.Navigation("Product");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("OnlineMenu.Models.Table", b =>
                {
                    b.HasOne("OnlineMenu.Models.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("OnlineMenu.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("OnlineMenu.Models.Order", b =>
                {
                    b.Navigation("ProductOrders");
                });

            modelBuilder.Entity("OnlineMenu.Models.Product", b =>
                {
                    b.Navigation("ProductOrders");
                });

            modelBuilder.Entity("OnlineMenu.Models.Table", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("ProductOrders");
                });

            modelBuilder.Entity("OnlineMenu.Models.Tenant", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Orders");

                    b.Navigation("ProductOrders");

                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}