using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using DAL.Data.Configuration;
using System.ComponentModel.Design.Serialization;
using System.Reflection.PortableExecutable;

namespace DAL.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<FeatureItem> FeatureItem { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DataContext() : base() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<FeatureItem>()
            //.HasOne(fi => fi.Item)
            //.WithMany(i => i.FeatureItem)
            //.HasForeignKey(fi => fi.ItemId);

            //modelBuilder.Entity<FeatureItem>()
            //    .HasOne(fi => fi.Feature)
            //    .WithMany(f => f.FeatureItem)
            //    .HasForeignKey(fi => fi.FeatureId);

            modelBuilder.Entity<Item>()
        .HasMany(e => e.Features)
        .WithMany(e => e.Items)
        .UsingEntity<FeatureItem>();


            modelBuilder.Entity<Feature>()
        .HasMany(e => e.Items)
        .WithMany(e => e.Features)
        .UsingEntity<FeatureItem>();

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            DataSeed(modelBuilder);
        }
        private void DataSeed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Characteristic>().HasData(
                new Characteristic { Id = 1, Name = "Genre" },
                new Characteristic { Id = 2, Name = "Language" }
                );

            modelBuilder.Entity<Feature>().HasData(
         new Feature { Id = 1, Name = "Women", CharacteristicsId = 1 },
         new Feature { Id = 2, Name = "Man", CharacteristicsId = 1 },
         new Feature { Id = 3, Name = "English", CharacteristicsId = 2 },
         new Feature { Id = 4, Name = "Ukrainian", CharacteristicsId = 2 },
         new Feature { Id = 5, Name = "Korean", CharacteristicsId = 2 }
         );

            modelBuilder.Entity<Item>().HasData(
                new Item { Id = 1, Name = "Anna"},
                 new Item { Id = 2, Name = "Bob"},
                 new Item { Id = 3, Name = "July" }
                );

            modelBuilder.Entity<FeatureItem>().HasData(
                new FeatureItem { Id = 1, ItemId = 1, FeatureId = 1 },
                new FeatureItem { Id = 2, ItemId = 1, FeatureId = 3 },
                new FeatureItem { Id = 3, ItemId = 1, FeatureId = 4 },
                new FeatureItem { Id = 4, ItemId = 2, FeatureId = 2 },
                new FeatureItem { Id = 5, ItemId = 2, FeatureId = 5 },
                new FeatureItem { Id = 6, ItemId = 3, FeatureId = 1 }

                );
        }
    }
}
