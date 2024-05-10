using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class FeatureRepository : BaseRepository<Feature>, IFeatureRepository
    {


        public FeatureRepository(DataContext context) : base(context)
        {
        }
    }
}
