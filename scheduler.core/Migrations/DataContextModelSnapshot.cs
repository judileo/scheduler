﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using scheduler.core.Data;

namespace scheduler.core.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("scheduler.core.Entities.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Begin")
                        .HasColumnType("text");

                    b.Property<int>("Day")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("End")
                        .HasColumnType("text");

                    b.Property<int>("EventStatusId")
                        .HasColumnType("integer");

                    b.Property<int>("FreeSlots")
                        .HasColumnType("integer");

                    b.Property<string>("InstructorId")
                        .HasColumnType("text");

                    b.Property<int>("MaxCapacity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EventStatusId");

                    b.HasIndex("InstructorId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("scheduler.core.Entities.EventStatus", b =>
                {
                    b.Property<int>("EventStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("EventStatusName")
                        .HasColumnType("text");

                    b.HasKey("EventStatusId");

                    b.ToTable("EventStatus");

                    b.HasData(
                        new
                        {
                            EventStatusId = 200,
                            EventStatusName = "Available"
                        },
                        new
                        {
                            EventStatusId = 400,
                            EventStatusName = "Cancelled"
                        },
                        new
                        {
                            EventStatusId = 500,
                            EventStatusName = "Deleted"
                        });
                });

            modelBuilder.Entity("scheduler.core.Entities.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 100,
                            Name = "Student"
                        },
                        new
                        {
                            Id = 105,
                            Name = "Instructor"
                        },
                        new
                        {
                            Id = 70,
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("scheduler.core.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("EventId")
                        .HasColumnType("uuid");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<int?>("RolId")
                        .HasColumnType("integer");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("RolId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("scheduler.core.Entities.Event", b =>
                {
                    b.HasOne("scheduler.core.Entities.EventStatus", "EventStatus")
                        .WithMany()
                        .HasForeignKey("EventStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("scheduler.core.Entities.User", "Instructor")
                        .WithMany()
                        .HasForeignKey("InstructorId");

                    b.Navigation("EventStatus");

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("scheduler.core.Entities.User", b =>
                {
                    b.HasOne("scheduler.core.Entities.Event", null)
                        .WithMany("Students")
                        .HasForeignKey("EventId");

                    b.HasOne("scheduler.core.Entities.Rol", "Rol")
                        .WithMany()
                        .HasForeignKey("RolId");

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("scheduler.core.Entities.Event", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
