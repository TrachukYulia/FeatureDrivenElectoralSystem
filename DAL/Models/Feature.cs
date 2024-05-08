using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Feature: BaseEntity
    {
        public string? Name { get; set; }
        public int CharacteristicsId { get; set; }  
        public Characteristics Characteristic { get; set; }
        public ICollection<Item>? Items { get; set; }
       public ICollection<FeatureItem>? FeatureItem { get; set; }
    }
}
