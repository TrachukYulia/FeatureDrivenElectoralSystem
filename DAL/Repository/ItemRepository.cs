using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        internal DataContext _db;
        public ItemRepository(DataContext context) : base(context)
        {
            _db = context;
        }
        public new void Create(Item item)
        {
            //foreach (var characteristicId in item..Keys)
            //{
            //    var featureId = item.SelectedFeatures[characteristicId];
            //    _context.ItemFeatures.Add(new ItemFeature { ItemId = item.ItemId, FeatureId = featureId });
            //}
            _db.Items.Add(item);

            // Добавляем связи Item-Feature в контекст данных
            foreach (var itemFeature in item.FeatureItem)
            {
                itemFeature.ItemId = item.Id;

                _db.FeatureItem.Add(itemFeature);
            }
            //_db.SaveChanges();
        }
    }
}
