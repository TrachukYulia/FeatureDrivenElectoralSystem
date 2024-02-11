using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICharacteristicService
    {
        IEnumerable<CharacteristicRequest> GetAll();
        void Create(CharacteristicRequest characteristicRequest);
        CharacteristicRequest Get(int id);
        void Update(CharacteristicRequest characteristicRequest, int id);
        void Delete(int id);
    }
}
