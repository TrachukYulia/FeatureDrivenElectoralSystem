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
    public class FeatureConfiguration<T> : IEntityTypeConfiguration<Feature> where T : Feature
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.Property(o => o.Name).HasMaxLength(200);
        }
    }
}

