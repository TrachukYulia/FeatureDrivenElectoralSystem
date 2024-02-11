using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repository
{
    public class CharacteristicRepository : BaseRepository<Characteristic>, ICharacteristicRepository
    {
        public CharacteristicRepository(DataContext context) : base(context)
        {
        }
    }
}
