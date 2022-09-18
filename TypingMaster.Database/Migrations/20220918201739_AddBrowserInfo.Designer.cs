﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TypingMaster.Database;

#nullable disable

namespace TypingMaster.Database.Migrations
{
    [DbContext(typeof(TestDbContext))]
    [Migration("20220918201739_AddBrowserInfo")]
    partial class AddBrowserInfo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.8");

            modelBuilder.Entity("TypingMaster.Database.Entities.TestEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("ExecutorName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("InorrectClicks")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("IsAndroid")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("IsDesktop")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("IsIPad")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("IsIPadPro")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("IsIPhone")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("IsMobile")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("IsTablet")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.Property<string>("OsName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TestDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TestId")
                        .HasColumnType("TEXT");

                    b.Property<int>("TestType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TextToRewritten")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TotalClicks")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Tests");
                });
#pragma warning restore 612, 618
        }
    }
}
