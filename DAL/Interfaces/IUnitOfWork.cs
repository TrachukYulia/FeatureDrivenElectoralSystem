using DAL.Models;
using DAL.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
        ItemRepository ItemRepository { get; }
        void Save();
    }
}
