﻿// <auto-generated />
using System;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("Investiments.Domain.Investiment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Value")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("WalletId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Type");

                    b.HasIndex("WalletId");

                    b.ToTable("Investiment", (string)null);

                    b.HasDiscriminator<string>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Wallets.Domain.Wallet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Wallet", (string)null);
                });

            modelBuilder.Entity("Bonds.Domain.Bond", b =>
                {
                    b.HasBaseType("Investiments.Domain.Investiment");

                    b.Property<decimal>("AnnualInterestRate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Expiration")
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("Bond");
                });

            modelBuilder.Entity("Stocks.Domain.Stock", b =>
                {
                    b.HasBaseType("Investiments.Domain.Investiment");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("Stock");
                });

            modelBuilder.Entity("Investiments.Domain.Investiment", b =>
                {
                    b.HasOne("Wallets.Domain.Wallet", "Wallet")
                        .WithMany()
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Wallet");
                });
#pragma warning restore 612, 618
        }
    }
}
