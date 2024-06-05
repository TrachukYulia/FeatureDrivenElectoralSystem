using BLL.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{ 
    public interface IItemService
    {
        IEnumerable<ItemRespond> GetAll();
        IEnumerable<ItemRespond> GetGeneticSolve(List<int> features);
        IEnumerable<ItemRespond> GetGreedySolve(List<int> features);
        byte[] ExportItemsToCsv(IEnumerable<ItemRespond> items);
        void Create(ItemRequest item);
        ItemRequest Get(int id);
        void Update(ItemRequest itemRequest, int id);
        void Delete(int id);

    }
}
