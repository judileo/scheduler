﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using scheduler.core.Data;

namespace scheduler.core.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220920022614_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("FreeSlots")
                        .HasColumnType("integer");

                    b.Property<Guid?>("InstructorId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("InstructorId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("scheduler.core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<Guid?>("EventId")
                        .HasColumnType("uuid");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Rol")
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("scheduler.core.Entities.Event", b =>
                {
                    b.HasOne("scheduler.core.Entities.User", "Instructor")
                        .WithMany()
                        .HasForeignKey("InstructorId");

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("scheduler.core.Entities.User", b =>
                {
                    b.HasOne("scheduler.core.Entities.Event", null)
                        .WithMany("Students")
                        .HasForeignKey("EventId");
                });

            modelBuilder.Entity("scheduler.core.Entities.Event", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
