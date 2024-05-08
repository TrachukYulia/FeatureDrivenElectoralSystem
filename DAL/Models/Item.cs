using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Item: BaseEntity
    {
        public string? Name { get; set; }
        public ICollection<Feature>? Features { get; set; }
        public ICollection<FeatureItem>? FeatureItem { get; set; }

        //public ICollection<Characteristic>?  Characteristics {get; set; }
    }
}
