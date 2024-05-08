using DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using System.Buffers.Text;

namespace DAL.Data.Configuration
{

    public class FeatureItemConfiguration<T> :IEntityTypeConfiguration<FeatureItem> where T : FeatureItem
    {
        public void Configure(ModelBuilder builder)
        {
          
        }

        public void Configure(EntityTypeBuilder<FeatureItem> builder)
        {
            throw new NotImplementedException();
        }
    }
}
