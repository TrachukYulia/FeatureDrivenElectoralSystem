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
        IEnumerable<CharacteristicRespond> GetAll();
        void Create(CharacteristicRequest characteristicRequest);
        CharacteristicNameRespond Get(int id);
        void Update(CharacteristicRequest characteristicRequest, int id);
        void Delete(int id);
    }
}
