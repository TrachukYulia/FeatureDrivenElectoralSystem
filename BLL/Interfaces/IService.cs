using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IService
    {
        public IFeatureService FeatureService { get; }
        public IItemService ItemService { get; }
        public ICharacteristicService CharacteristicService { get; }
    }
}
