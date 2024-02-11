using AutoMapper;
using BLL.DTO;
using BLL.Exception;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CharacteristicService : ICharacteristicService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CharacteristicService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public void Create(CharacteristicRequest characteristicRequest)
        {
            if (characteristicRequest is null)
                throw new ArgumentNullException(nameof(characteristicRequest), message: "Object is empty");
            var item = _mapper.Map<Characteristic>(characteristicRequest);
            _unitOfWork.GetRepository<Characteristic>().Create(item);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            var characteristic = _unitOfWork.GetRepository<Characteristic>().Get(id);

            if (characteristic is null)
                throw new NotFoundException("Object with id {id} not found");

            _unitOfWork.GetRepository<Characteristic>().Delete(characteristic);
            _unitOfWork.Save();
        }

        public CharacteristicRequest Get(int id)
        {
            var characteristic = _unitOfWork.GetRepository<Characteristic>().Get(id);

            if (characteristic == null)
            {
                throw new NotFoundException("Object with id {id} not found");
            }

            return _mapper.Map<CharacteristicRequest>(characteristic);
        }

        public IEnumerable<CharacteristicRequest> GetAll()
        {
            var characteristics = _unitOfWork.GetRepository<Characteristic>().GetAll();
            if (characteristics is null)
                throw new NotFoundException("List is empty");
            return _mapper.Map<IEnumerable<CharacteristicRequest>>(characteristics);
        }

        public void Update(CharacteristicRequest characteristicRequest, int id)
        {

            var characteristic = _unitOfWork.GetRepository<Characteristic>().Get(id);

            if (characteristic == null)
            {
                throw new NotFoundException("Object not found");
            }

            characteristic = _mapper.Map(characteristicRequest, characteristic);

            _unitOfWork.GetRepository<Characteristic>().Update(characteristic);
            _unitOfWork.Save();
        }

    }
}
