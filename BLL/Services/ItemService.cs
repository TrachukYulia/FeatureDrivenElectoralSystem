using AutoMapper;
using BLL.DTO;
using BLL.Exception;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
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
            //foreach (var featureItem in item.FeatureItem)
            //{
            //    featureItem.Item = item;
            //}
            //foreach (var characteristicId in item.SelectedFeatures.Keys)
            //{
            //    var featureId = item.SelectedFeatures[characteristicId];
            //    _context.ItemFeatures.Add(new ItemFeature { ItemId = item.ItemId, FeatureId = featureId });
            //}
            //_unitOfWork.GetRepository<Item>().Create(item);
            _unitOfWork.ItemRepository.Create(item);
            _unitOfWork.Save();
        }

        public IEnumerable<ItemRespond> GetAll()
        {
            var items = _unitOfWork.GetRepository<Item>().GetAll();
            var characteristics = _unitOfWork.GetRepository<Characteristic>().GetAll();
            var featureItems = _unitOfWork.GetRepository<FeatureItem>().GetAll();


            if (items is null)
                throw new NotFoundException("List is empty");
 
            return _mapper.Map<IEnumerable<ItemRespond>>(items);
        }
        public IEnumerable<ItemRespond> GetGeneticSolve()
        {
            var items = _unitOfWork.GetRepository<Item>().GetAll();
            var characteristics = _unitOfWork.GetRepository<Characteristic>().GetAll();
            var featureItems = _unitOfWork.GetRepository<FeatureItem>().GetAll();
            var features = _unitOfWork.GetRepository<Feature>().GetAll();

            if (items is null)
                throw new NotFoundException("List is empty");

            int[,] matrix = new int[items.Count(), features.Count()];

            // Заполнение матрицы
            for (int i = 0; i < items.Count(); i++)
            {
                for (int j = 0; j < features.Count(); j++)
                {
                    // Проверяем, есть ли у текущего элемента данная характеристика
                    matrix[i, j] = items.ElementAt(i).Features.Any(f => f.Id == features.ElementAt(j).Id) ? 1 : 0;
                }
            }
            var geneticAlgo = new GeneticAlgo(matrix);
            var validMatrix = geneticAlgo.VakidMatrix;
            var population = geneticAlgo.Population;
            var indexOfItems = geneticAlgo.GetSolutionByGeneticAlgWithPrint(validMatrix, population);
         
            List<Item> filteredItems = new List<Item>();

            // Пройдите по индексам элементов
            foreach (var index in indexOfItems)
            {
                // Проверьте, существует ли элемент с таким индексом в списке items
                if (index < items.Count())
                {
                    // Если элемент существует, добавьте его в отфильтрованный список
                    filteredItems.Add(items.ElementAt(index));
                }
            }
            return _mapper.Map<IEnumerable<ItemRespond>>(filteredItems);
        }
        public IEnumerable<ItemRespond> GetGreedySolve(List<int> features)
        {
            var items = _unitOfWork.GetRepository<Item>().GetAll();
            var characteristics = _unitOfWork.GetRepository<Characteristic>().GetAll();
            var featureItems = _unitOfWork.GetRepository<FeatureItem>().GetAll();
           var features2 = _unitOfWork.GetRepository<Feature>().GetAll();

            if (items is null)
                throw new NotFoundException("List of items is empty");
            if (features is null)
                throw new NotFoundException("You did not select any feature");
            int[,] matrix = new int[items.Count(), features.Count()];

            // Заполнение матрицы
            for (int i = 0; i < items.Count(); i++)
            {
                for (int j = 0; j < features.Count(); j++)
                {
                    // Проверяем, есть ли у текущего элемента данная характеристика
                    matrix[i, j] = items.ElementAt(i).Features.Any(f => f.Id == features2.ElementAt(j).Id) ? 1 : 0;
                }
            }
            var greedyAlg = new GreedyAlg(matrix);
            var indexOfItems = greedyAlg.Solve();

            List<Item> filteredItems = new List<Item>();

            // Пройдите по индексам элементов
            foreach (var index in indexOfItems)
            {
                // Проверьте, существует ли элемент с таким индексом в списке items
                if (index < items.Count())
                {
                    // Если элемент существует, добавьте его в отфильтрованный список
                    filteredItems.Add(items.ElementAt(index));
                }
            }
            return _mapper.Map<IEnumerable<ItemRespond>>(filteredItems);
        }


        public ItemRequest Get(int id)
        {
            // var item = _unitOfWork.GetRepository<Item>().Get(id);
            var item = _unitOfWork.ItemRepository.Get(id);

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

            // _unitOfWork.ItemRepository.Update(item);
            _unitOfWork.GetRepository<Item>().Update(item);

            _unitOfWork.Save();
        }
        public void GetGeneticAlgo ()
        {
            Console.WriteLine("Hello world");

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
