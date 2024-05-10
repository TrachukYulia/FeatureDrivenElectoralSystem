using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class FeatureModel
    {
        public int Id { get; set; }
        public string? FeatureName { get; set; }
        //public IEnumerable<IEnumerable<string>?> Features { get; set; }
        public int CharacteristicsId { get; set; }
        // public ICollection<string>? ItemName { get; set; }
       // public string? ItemName { get; set; }
        //public List<string?> ItemNames { get; internal set; }
        public string? CharacteristicName { get; set; }

        //public string? CharasticName { get; set; }
    }
}
