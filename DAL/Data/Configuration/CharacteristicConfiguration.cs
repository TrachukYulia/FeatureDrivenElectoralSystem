using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Models;
using System.Threading.Tasks;

namespace DAL.Data.Configuration
{
    
    public class CharacteristicConfiguration<T> : IEntityTypeConfiguration<Characteristic> where T : Characteristic
    {
        public void Configure(EntityTypeBuilder<Characteristic> builder)
        {
            builder.Property(o => o.Name).HasMaxLength(200).IsRequired();
        }
    }
}
