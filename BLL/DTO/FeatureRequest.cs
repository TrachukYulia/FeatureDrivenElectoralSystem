using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class FeatureRequest
    {
        public string? FeatureName { get; set; }
        //public Characteristics Characteristic { get; set; }
        public int CharacteristicId { get; set; }


    }
}
