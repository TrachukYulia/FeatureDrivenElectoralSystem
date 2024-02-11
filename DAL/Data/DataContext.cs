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

namespace DAL.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DataContext() : base() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            new ItemConfiguration<Item>().Configure(modelBuilder.Entity<Item>());
            new FeatureConfiguration<Feature>().Configure(modelBuilder.Entity<Feature>());
            new CharacteristicConfiguration<Characteristic>().Configure(modelBuilder.Entity<Characteristic>());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
