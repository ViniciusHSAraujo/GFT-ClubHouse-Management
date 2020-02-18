﻿// <auto-generated />
using System;
using GFT_ClubHouse__Management.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GFT_ClubHouse__Management.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200218123758_RemoveIsSoldFromTicket")]
    partial class RemoveIsSoldFromTicket
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GFT_ClubHouse__Management.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(12);

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<string>("Zip")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Addresses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "Seattle",
                            State = "Washington",
                            Street = "1234 1St Ave",
                            Zip = "98101"
                        });
                });

            modelBuilder.Entity("GFT_ClubHouse__Management.Models.ClubHouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddressId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("ClubHouses");
                });

            modelBuilder.Entity("GFT_ClubHouse__Management.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Capacity");

                    b.Property<int>("ClubHouseId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("MusicalGenreId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<double>("Price");

                    b.HasKey("Id");

                    b.HasIndex("ClubHouseId");

                    b.HasIndex("MusicalGenreId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("GFT_ClubHouse__Management.Models.MusicalGenre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.HasKey("Id");

                    b.ToTable("MusicalGenres");
                });

            modelBuilder.Entity("GFT_ClubHouse__Management.Models.Sale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EventId");

                    b.Property<int>("Quantity");

                    b.Property<double>("SinglePrice");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("GFT_ClubHouse__Management.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EventId");

                    b.Property<Guid?>("Hash");

                    b.Property<int?>("SaleId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("SaleId");

                    b.HasIndex("UserId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("GFT_ClubHouse__Management.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddressId");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("LastName");

                    b.Property<string>("Name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.Property<int>("Roles");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddressId = 1,
                            Email = "admin@admin.com",
                            LastName = "Default",
                            Name = "Admin",
                            Password = "690e2695b6aa8f08dc1fd736072e5819",
                            Phone = "123456789",
                            Roles = 0
                        });
                });

            modelBuilder.Entity("GFT_ClubHouse__Management.Models.ClubHouse", b =>
                {
                    b.HasOne("GFT_ClubHouse__Management.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GFT_ClubHouse__Management.Models.Event", b =>
                {
                    b.HasOne("GFT_ClubHouse__Management.Models.ClubHouse", "ClubHouse")
                        .WithMany()
                        .HasForeignKey("ClubHouseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GFT_ClubHouse__Management.Models.MusicalGenre", "MusicalGenre")
                        .WithMany()
                        .HasForeignKey("MusicalGenreId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GFT_ClubHouse__Management.Models.Sale", b =>
                {
                    b.HasOne("GFT_ClubHouse__Management.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GFT_ClubHouse__Management.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GFT_ClubHouse__Management.Models.Ticket", b =>
                {
                    b.HasOne("GFT_ClubHouse__Management.Models.Event", "Event")
                        .WithMany("Tickets")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GFT_ClubHouse__Management.Models.Sale", "Sale")
                        .WithMany("Tickets")
                        .HasForeignKey("SaleId");

                    b.HasOne("GFT_ClubHouse__Management.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("GFT_ClubHouse__Management.Models.User", b =>
                {
                    b.HasOne("GFT_ClubHouse__Management.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
