using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyAccess.Infrastructure
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(params object[] id);
        void Insert(TEntity entity);
        void Delete(params object[] id);
        void Update(TEntity entity);
        void Save();
    }
}
