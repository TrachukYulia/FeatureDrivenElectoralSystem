using AutoMapper;
using BLL.Interfaces;
using DAL.Models;
using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using BLL.Exception;
using System.Reflection.PortableExecutable;

namespace BLL.Services
{
    public class FeatureService: IFeatureService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public FeatureService(IUnitOfWork unitOfWork, IMapper mapper) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Create(FeatureRequest item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item), message: "Object is empty");
            var feature = _mapper.Map<Feature>(item);
            _unitOfWork.GetRepository<Feature>().Create(feature);
            _unitOfWork.Save();
        }

        public IEnumerable<FeatureModel> GetAll()
        {
            var features = _unitOfWork.GetRepository<Feature>().GetAll();
            var featureItems = _unitOfWork.GetRepository<FeatureItem>().GetAll();
            var characteristics = _unitOfWork.GetRepository<Characteristic>().GetAll();
            var items = _unitOfWork.GetRepository<Item>().GetAll();

            if (features is null)
                throw new NotFoundException("List is empty");
            var featureModels = _mapper.Map<List<FeatureModel>>(features);
            foreach (var featureModel in featureModels)
            {
              //  var itemNames = items.Where(item => item.Features.Any(f => f.Name == featureModel.FeatureName)).Select(item => item.Name).ToList();
                //     var characteristicName = features.Where(y => characteristic.Any(x => x.Id == y.CharacteristicsId)).Select(r => r.Name).First();
                var featureId = features.First(f => f.Name == featureModel.FeatureName)?.Id;
                var characteristicId = features.FirstOrDefault(f => f.Name == featureModel.FeatureName)?.CharacteristicsId;
                var characteristicName = characteristics.FirstOrDefault(ch => ch.Id == characteristicId)?.Name;
               /// featureModel.ItemNames = itemNames;
                featureModel.CharacteristicName = characteristicName;
               
            }
            return featureModels;
                //_mapper.Map<IEnumerable<FeatureModel>>(features);
        }
        public IEnumerable<FeatureRespond> GetFeatureName()
        {

            var features = _unitOfWork.GetRepository<Feature>().GetAll();
          
            var featureModels = _mapper.Map<List<FeatureRespond>>(features);

            return featureModels;
            //_mapper.Map<IEnumerable<FeatureModel>>(features);
        }
        public FeatureRequest Get(int id)
        {
            var feature = _unitOfWork.GetRepository<Feature>().Get(id);

            if (feature == null)
            {
                throw new NotFoundException("Object with id {id} not found");
            }

            return _mapper.Map<FeatureRequest>(feature);
        }
        public void Update(FeatureRequest item, int id)
        {
            var feature = _unitOfWork.GetRepository<Feature>().Get(id);

            if (feature == null)
            {
                throw new NotFoundException("Object not found");
            }

            feature = _mapper.Map(item, feature);

            _unitOfWork.GetRepository<Feature>().Update(feature);
            _unitOfWork.Save();
        }
        public void Delete(int id)
        {
            var feature = _unitOfWork.GetRepository<Feature>().Get(id);

            if (feature is null)
                throw new NotFoundException("Object with id {id} not found");

            _unitOfWork.GetRepository<Feature>().Delete(feature);
            _unitOfWork.Save();
        }

    }
}
