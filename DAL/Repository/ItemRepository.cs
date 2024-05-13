using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace DAL.Repository
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        internal DataContext _db;
        public ItemRepository(DataContext context) : base(context)
        {
            _db = context;
        }
        public new void Update(Item item)
        {

            // Получаем все существующие записи FeatureItem для текущего элемента Item
            //var existingFeatures = _db.FeatureItem.Where(fi => fi.ItemId == item.Id).ToList();

            //// Обновляем или удаляем существующие записи FeatureItem
            //foreach (var existingFeature in existingFeatures)
            //{
            //    // Находим соответствующий элемент FeatureItem в списке item.FeatureItem
            //    var newItemFeature = item.FeatureItem.FirstOrDefault(nf => nf.Id == existingFeature.Id);
            //    if (newItemFeature != null)
            //    {
            //        // Обновляем значение featureId для существующего элемента FeatureItem
            //        existingFeature.FeatureId = newItemFeature.FeatureId;

            //        _db.FeatureItem.Update(existingFeature);
            //    }
             
            //}

            _db.Items.Update(item);


        }

        public new void Create(Item item)
        {
            _db.Items.Add(item);
            // Добавляем связи Item-Feature в контекст данных
            foreach (var itemFeature in item.FeatureItem)
            {
                itemFeature.ItemId = item.Id;

                _db.FeatureItem.Add(itemFeature);
            }
        }
    }
}
