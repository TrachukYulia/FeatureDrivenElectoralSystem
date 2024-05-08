using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
     public class FeatureItem: BaseEntity
    {
        public int FeatureId{ get; set; }
        public int ItemId { get; set; }
        public Item? Item { get; set; }
       public Feature? Feature { get; set; }
    }
}
