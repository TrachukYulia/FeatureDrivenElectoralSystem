﻿// <auto-generated />
using System;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FeatureDrivenElectoralSystemApi.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Characteristics");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Genre"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Language"
                        });
                });

            modelBuilder.Entity("DAL.Models.Feature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Characteristic")
                        .HasColumnType("integer");

                    b.Property<int?>("CharacteristicId")
                        .HasColumnType("integer");

                    b.Property<int>("CharacteristicsId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CharacteristicId");

                    b.ToTable("Features");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Characteristic = 0,
                            CharacteristicsId = 1,
                            Name = "Women"
                        },
                        new
                        {
                            Id = 2,
                            Characteristic = 0,
                            CharacteristicsId = 1,
                            Name = "Man"
                        },
                        new
                        {
                            Id = 3,
                            Characteristic = 0,
                            CharacteristicsId = 2,
                            Name = "English"
                        },
                        new
                        {
                            Id = 4,
                            Characteristic = 0,
                            CharacteristicsId = 2,
                            Name = "Ukrainian"
                        },
                        new
                        {
                            Id = 5,
                            Characteristic = 0,
                            CharacteristicsId = 2,
                            Name = "Korean"
                        });
                });

            modelBuilder.Entity("DAL.Models.FeatureItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("FeatureId")
                        .HasColumnType("integer");

                    b.Property<int>("ItemId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FeatureId");

                    b.HasIndex("ItemId");

                    b.ToTable("FeatureItem");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FeatureId = 1,
                            ItemId = 1
                        },
                        new
                        {
                            Id = 2,
                            FeatureId = 3,
                            ItemId = 1
                        },
                        new
                        {
                            Id = 3,
                            FeatureId = 4,
                            ItemId = 1
                        },
                        new
                        {
                            Id = 4,
                            FeatureId = 2,
                            ItemId = 2
                        },
                        new
                        {
                            Id = 5,
                            FeatureId = 5,
                            ItemId = 2
                        },
                        new
                        {
                            Id = 6,
                            FeatureId = 1,
                            ItemId = 3
                        });
                });

            modelBuilder.Entity("DAL.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Anna"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Bob"
                        },
                        new
                        {
                            Id = 3,
                            Name = "July"
                        });
                });

            modelBuilder.Entity("DAL.Models.Feature", b =>
                {
                    b.HasOne("DAL.Models.Characteristic", null)
                        .WithMany("Features")
                        .HasForeignKey("CharacteristicId");
                });

            modelBuilder.Entity("DAL.Models.FeatureItem", b =>
                {
                    b.HasOne("DAL.Models.Feature", "Feature")
                        .WithMany("FeatureItem")
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.Item", "Item")
                        .WithMany("FeatureItem")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Feature");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("DAL.Models.Characteristic", b =>
                {
                    b.Navigation("Features");
                });

            modelBuilder.Entity("DAL.Models.Feature", b =>
                {
                    b.Navigation("FeatureItem");
                });

            modelBuilder.Entity("DAL.Models.Item", b =>
                {
                    b.Navigation("FeatureItem");
                });
#pragma warning restore 612, 618
        }
    }
}
