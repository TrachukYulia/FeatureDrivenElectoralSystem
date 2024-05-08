using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class FeatureItemRepository : BaseRepository<FeatureItem>, IFeatureItemRepository
    {
        public FeatureItemRepository(DataContext context) : base(context)
        {
        }
    }
}
