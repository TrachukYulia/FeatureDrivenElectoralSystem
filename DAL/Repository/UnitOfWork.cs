using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
            if (_repositories == null)
                _repositories = new Hashtable();

        }
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            var Type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(Type))
            {
                var repositiryType = typeof(BaseRepository<>);
                var repositoryInstance = Activator.CreateInstance(
                    repositiryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(Type, repositoryInstance);
            }
            return (IRepository<TEntity>)_repositories[Type];
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
