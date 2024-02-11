﻿// <auto-generated />
using System;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FeatureDrivenElectoralSystemApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240128003756_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DAL.Models.Characteristic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ItemId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("Characteristics");
                });

            modelBuilder.Entity("DAL.Models.Feature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CharacteristicId")
                        .HasColumnType("integer");

                    b.Property<int>("Characteristics")
                        .HasColumnType("integer");

                    b.Property<int>("CharacteristicsId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.HasIndex("CharacteristicId");

                    b.ToTable("Features");
                });

            modelBuilder.Entity("DAL.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("DAL.Models.Characteristic", b =>
                {
                    b.HasOne("DAL.Models.Item", "Item")
                        .WithMany("Characteristics")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("DAL.Models.Feature", b =>
                {
                    b.HasOne("DAL.Models.Characteristic", null)
                        .WithMany("Features")
                        .HasForeignKey("CharacteristicId");
                });

            modelBuilder.Entity("DAL.Models.Characteristic", b =>
                {
                    b.Navigation("Features");
                });

            modelBuilder.Entity("DAL.Models.Item", b =>
                {
                    b.Navigation("Characteristics");
                });
#pragma warning restore 612, 618
        }
    }
}
