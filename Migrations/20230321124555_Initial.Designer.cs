﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Odem.WebAPI;

#nullable disable

namespace Odem.WebAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230321124555_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Odem.WebAPI.Models.Address", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Odem.WebAPI.Models.Admin", b =>
                {
                    b.Property<string>("Uid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AddressId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Uid");

                    b.HasIndex("AddressId");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Odem.WebAPI.Models.BankTransfer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("BankTransfers");
                });

            modelBuilder.Entity("Odem.WebAPI.Models.Client", b =>
                {
                    b.Property<string>("Uid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AddressId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("WalletId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Uid");

                    b.HasIndex("AddressId");

                    b.HasIndex("WalletId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Odem.WebAPI.Models.LocalTransfer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("LocalTransfers");
                });

            modelBuilder.Entity("Odem.WebAPI.Models.OdemTransfer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FromId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ToId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("FromId");

                    b.HasIndex("ToId");

                    b.ToTable("OdemTransfers");
                });

            modelBuilder.Entity("Odem.WebAPI.Models.Ticket", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CloseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedByUid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("HandledByUid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUid");

                    b.HasIndex("HandledByUid");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Odem.WebAPI.Models.Wallet", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("Odem.WebAPI.Models.Admin", b =>
                {
                    b.HasOne("Odem.WebAPI.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Odem.WebAPI.Models.Client", b =>
                {
                    b.HasOne("Odem.WebAPI.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("Odem.WebAPI.Models.Wallet", "Wallet")
                        .WithMany()
                        .HasForeignKey("WalletId");

                    b.Navigation("Address");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("Odem.WebAPI.Models.OdemTransfer", b =>
                {
                    b.HasOne("Odem.WebAPI.Models.Wallet", "From")
                        .WithMany()
                        .HasForeignKey("FromId");

                    b.HasOne("Odem.WebAPI.Models.Wallet", "To")
                        .WithMany()
                        .HasForeignKey("ToId");

                    b.Navigation("From");

                    b.Navigation("To");
                });

            modelBuilder.Entity("Odem.WebAPI.Models.Ticket", b =>
                {
                    b.HasOne("Odem.WebAPI.Models.Client", "CreatedBy")
                        .WithMany("Tickets")
                        .HasForeignKey("CreatedByUid");

                    b.HasOne("Odem.WebAPI.Models.Admin", "HandledBy")
                        .WithMany("HandledTickets")
                        .HasForeignKey("HandledByUid");

                    b.Navigation("CreatedBy");

                    b.Navigation("HandledBy");
                });

            modelBuilder.Entity("Odem.WebAPI.Models.Admin", b =>
                {
                    b.Navigation("HandledTickets");
                });

            modelBuilder.Entity("Odem.WebAPI.Models.Client", b =>
                {
                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}