﻿// <auto-generated />
using System;
using EcommerceApiTest.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EcommerceApiTest.Migrations
{
    [DbContext(typeof(EcommerceContext))]
    [Migration("20220809184506_InitECommerceDb")]
    partial class InitECommerceDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EcommerceApiTest.Model.Picture", b =>
                {
                    b.Property<int>("PictureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PictureId"), 1L, 1);

                    b.Property<byte[]>("Pictures")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int?>("ProductRef")
                        .HasColumnType("int");

                    b.Property<DateTime>("UplodeDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PictureId");

                    b.HasIndex("ProductRef");

                    b.ToTable("Picture");
                });

            modelBuilder.Entity("JwtAuthntication.Model.Catigorie", b =>
                {
                    b.Property<int>("CatigorieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CatigorieId"), 1L, 1);

                    b.Property<string>("CatigorieDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CatigorieName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CatigorieId");

                    b.ToTable("Catigorie");
                });

            modelBuilder.Entity("JwtAuthntication.Model.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OredrDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("customer")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("JwtAuthntication.Model.Product", b =>
                {
                    b.Property<int>("Ref")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Ref"), 1L, 1);

                    b.Property<string>("Descreption")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("ProviderId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("SubCatigorieId")
                        .HasColumnType("int");

                    b.Property<int>("provider")
                        .HasColumnType("int");

                    b.Property<int>("subCatigorie")
                        .HasColumnType("int");

                    b.HasKey("Ref");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProviderId");

                    b.HasIndex("SubCatigorieId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("JwtAuthntication.Model.SubCatigorie", b =>
                {
                    b.Property<int>("SubCatigorieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubCatigorieId"), 1L, 1);

                    b.Property<int>("CatigorieId")
                        .HasColumnType("int");

                    b.Property<string>("SubCatigorieDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubCatigorieName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("catigorie")
                        .HasColumnType("int");

                    b.HasKey("SubCatigorieId");

                    b.HasIndex("CatigorieId");

                    b.ToTable("SubCatigorie");
                });

            modelBuilder.Entity("JwtAuthntication.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AspNetUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("JwtAuthntication.Model.Admin", b =>
                {
                    b.HasBaseType("JwtAuthntication.Model.User");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("JwtAuthntication.Model.Customer", b =>
                {
                    b.HasBaseType("JwtAuthntication.Model.User");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PictureId")
                        .HasColumnType("int");

                    b.HasIndex("PictureId");

                    b.HasDiscriminator().HasValue("Customer");
                });

            modelBuilder.Entity("JwtAuthntication.Model.Provider", b =>
                {
                    b.HasBaseType("JwtAuthntication.Model.User");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Matricule")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Service")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Provider");
                });

            modelBuilder.Entity("EcommerceApiTest.Model.Picture", b =>
                {
                    b.HasOne("JwtAuthntication.Model.Product", null)
                        .WithMany("Gallery")
                        .HasForeignKey("ProductRef");
                });

            modelBuilder.Entity("JwtAuthntication.Model.Order", b =>
                {
                    b.HasOne("JwtAuthntication.Model.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("JwtAuthntication.Model.Product", b =>
                {
                    b.HasOne("JwtAuthntication.Model.Order", null)
                        .WithMany("Products")
                        .HasForeignKey("OrderId");

                    b.HasOne("JwtAuthntication.Model.Provider", "Provider")
                        .WithMany("Products")
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JwtAuthntication.Model.SubCatigorie", "SubCatigorie")
                        .WithMany("products")
                        .HasForeignKey("SubCatigorieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Provider");

                    b.Navigation("SubCatigorie");
                });

            modelBuilder.Entity("JwtAuthntication.Model.SubCatigorie", b =>
                {
                    b.HasOne("JwtAuthntication.Model.Catigorie", "Catigorie")
                        .WithMany("SubCatigories")
                        .HasForeignKey("CatigorieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Catigorie");
                });

            modelBuilder.Entity("JwtAuthntication.Model.Customer", b =>
                {
                    b.HasOne("EcommerceApiTest.Model.Picture", "Picture")
                        .WithMany()
                        .HasForeignKey("PictureId");

                    b.Navigation("Picture");
                });

            modelBuilder.Entity("JwtAuthntication.Model.Catigorie", b =>
                {
                    b.Navigation("SubCatigories");
                });

            modelBuilder.Entity("JwtAuthntication.Model.Order", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("JwtAuthntication.Model.Product", b =>
                {
                    b.Navigation("Gallery");
                });

            modelBuilder.Entity("JwtAuthntication.Model.SubCatigorie", b =>
                {
                    b.Navigation("products");
                });

            modelBuilder.Entity("JwtAuthntication.Model.Provider", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
