﻿// <auto-generated />
using System;
using Back_end_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Back_end_API.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20230310143738_addImagesAndSinistresTables")]
    partial class addImagesAndSinistresTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Back_end_API.Models.Client", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Adress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssuranceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Back_end_API.Models.Image", b =>
                {
                    b.Property<int>("ImageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SinistreID")
                        .HasColumnType("int");

                    b.HasKey("ImageID");

                    b.HasIndex("SinistreID");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Back_end_API.Models.Sinistre", b =>
                {
                    b.Property<int>("SinistreID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SinistreID");

                    b.ToTable("Sinistres");
                });

            modelBuilder.Entity("Back_end_API.Models.Image", b =>
                {
                    b.HasOne("Back_end_API.Models.Sinistre", null)
                        .WithMany("Images")
                        .HasForeignKey("SinistreID");
                });

            modelBuilder.Entity("Back_end_API.Models.Sinistre", b =>
                {
                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
