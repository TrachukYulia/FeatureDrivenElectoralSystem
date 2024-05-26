using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class ItemRespond
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public ICollection<Feature>? Features { get; set; }
        //public ICollection<string> FeaturesName { get; set; }
        public ICollection<ItemFeatureRequest> FeatureItem { get; set; }
        //  public string Name { get; set; }
        // public List<string> CharacteristicValues { get; set; }
    }
}
