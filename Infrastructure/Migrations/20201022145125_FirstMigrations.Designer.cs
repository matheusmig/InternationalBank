﻿// <auto-generated />
using System;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(InternationalBankContext))]
    [Migration("20201022145125_FirstMigrations")]
    partial class FirstMigrations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Accounts.Account", b =>
                {
                    b.Property<Guid>("AccountId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("char(36)");

                    b.HasKey("AccountId");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            AccountId = new Guid("4c510cfe-5d61-4a46-a3d9-c4313426655f"),
                            Currency = "BRL",
                            CustomerId = new Guid("4c510cfe-5d61-4a46-a3d9-c4313426621f")
                        });
                });

            modelBuilder.Entity("Domain.Accounts.Credits.Credit", b =>
                {
                    b.Property<Guid>("CreditId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("CreditId");

                    b.HasIndex("AccountId");

                    b.ToTable("Credits");

                    b.HasData(
                        new
                        {
                            CreditId = new Guid("7bf066ba-379a-4e72-a59b-9755fda432ce"),
                            AccountId = new Guid("4c510cfe-5d61-4a46-a3d9-c4313426655f"),
                            Currency = "BRL",
                            TransactionDate = new DateTime(2020, 10, 22, 14, 51, 25, 470, DateTimeKind.Utc).AddTicks(6059),
                            Value = 400m
                        });
                });

            modelBuilder.Entity("Domain.Accounts.Debits.Debit", b =>
                {
                    b.Property<Guid>("DebitId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("DebitId");

                    b.HasIndex("AccountId");

                    b.ToTable("Debit");

                    b.HasData(
                        new
                        {
                            DebitId = new Guid("31ade963-bd69-4afb-9df7-611ae2cfa651"),
                            AccountId = new Guid("4c510cfe-5d61-4a46-a3d9-c4313426655f"),
                            Currency = "BRL",
                            TransactionDate = new DateTime(2020, 10, 22, 14, 51, 25, 470, DateTimeKind.Utc).AddTicks(7447),
                            Value = 50m
                        });
                });

            modelBuilder.Entity("Domain.Accounts.Credits.Credit", b =>
                {
                    b.HasOne("Domain.Accounts.Account", "Account")
                        .WithMany("CreditsCollection")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Accounts.Debits.Debit", b =>
                {
                    b.HasOne("Domain.Accounts.Account", "Account")
                        .WithMany("DebitsCollection")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
