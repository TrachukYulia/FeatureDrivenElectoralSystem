﻿using AutoMapper;
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
    public class ItemService: IItemService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Create(ItemRequest itemRequest)
        {
            if (itemRequest is null)
                throw new ArgumentNullException(nameof(itemRequest), message: "Object is empty");
            var item = _mapper.Map<Item>(itemRequest);
            _unitOfWork.GetRepository<Item>().Create(item);
            _unitOfWork.Save();
        }

        public IEnumerable<ItemRequest> GetAll()
        {
            var items = _unitOfWork.GetRepository<Item>().GetAll();
            if (items is null)
                throw new NotFoundException("List is empty");
            return _mapper.Map<IEnumerable<ItemRequest>>(items);
        }
        public ItemRequest Get(int id)
        {
            var item = _unitOfWork.GetRepository<Item>().Get(id);

            if (item == null)
            {
                throw new NotFoundException("Object with id {id} not found");
            }

            return _mapper.Map<ItemRequest>(item);
        }
        public void Update(ItemRequest itemRequest, int id)
        {
            var item = _unitOfWork.GetRepository<Item>().Get(id);

            if (item == null)
            {
                throw new NotFoundException("Object not found");
            }

            item = _mapper.Map(itemRequest, item);

            _unitOfWork.GetRepository<Item>().Update(item);
            _unitOfWork.Save();
        }
        public void Delete(int id)
        {
            var item = _unitOfWork.GetRepository<Item>().Get(id);

            if (item is null)
                throw new NotFoundException("Object with id {id} not found");

            _unitOfWork.GetRepository<Item>().Delete(item);
            _unitOfWork.Save();
        }
    }
}
