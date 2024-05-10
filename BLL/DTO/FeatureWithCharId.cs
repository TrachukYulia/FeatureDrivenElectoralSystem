using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class FeatureWithCharIdRespond
    {
        public int Id { get; set; }
        public string? FeatureName { get; set; }

        public int CharacteristicsId { get; set; }
        public string? CharacteristicName { get; set; }

    }
}
