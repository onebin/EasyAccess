using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyAccess.Infrastructure.RepositoryFramework
{
    interface IRepositoryBase<TEntity> : IRepository where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(params object[] id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(params object[] id);
    }
}
