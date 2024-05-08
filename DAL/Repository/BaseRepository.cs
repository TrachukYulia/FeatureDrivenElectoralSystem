using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        internal readonly DataContext dataContext;
        //public BaseRepository(DataContext context)
        //{
        //    dataContext = context;
        //}
        //void IRepository<T>.Create(T entity)
        //{
        //    dataContext.Add(entity);
        //}

        //void IRepository<T>.Delete(T entity)
        //{
        //    dataContext.Set<T>().Remove(entity);
        //}

        //T IRepository<T>.Get(int id)
        //{
        //    return dataContext.Set<T>().FirstOrDefault(x => x.Id == id);
        //}

        //IEnumerable<T> IRepository<T>.GetAll()
        //{
        //    return dataContext.Set<T>().ToList();
        //}

        //void IRepository<T>.Update(T entity)
        //{
        //    //dataContext.Update(entity);
        //    dataContext.Entry(entity).State = EntityState.Modified;

        //}

        public BaseRepository(DataContext context)
        {
            dataContext = context;
        }

        public void Create(T entity)
        {
            dataContext.Add(entity);
        }

        public void Update(T entity)
        {
            dataContext.Update(entity);
        }
        public void Delete(T entity)
        {
            dataContext.Set<T>().Remove(entity);
        }

        public T Get(int id)
        {
            return dataContext.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return dataContext.Set<T>().ToList();
        }

        
    }
}
