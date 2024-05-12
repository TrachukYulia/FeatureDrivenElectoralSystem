using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IFeatureService
    {
        IEnumerable<FeatureModel> GetAll();
        IEnumerable<FeatureRespond> GetFeatureName();

        void Create(FeatureRequest item);
        FeatureNameRespond Get(int id);
        void Update(FeatureRequest item, int id);
        void Delete(int id);

    }
}
