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

        public IEnumerable<FeatureRequest> GetAll()
        {
            var features = _unitOfWork.GetRepository<Feature>().GetAll();
            if (features is null)
                throw new NotFoundException("List is empty");
            return _mapper.Map<IEnumerable<FeatureRequest>>(features);
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
