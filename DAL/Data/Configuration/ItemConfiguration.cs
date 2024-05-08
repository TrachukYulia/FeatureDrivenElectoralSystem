using DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Configuration
{
    public class ItemConfiguration<T> : IEntityTypeConfiguration<Item> where T : Item
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
        }
    }
}
