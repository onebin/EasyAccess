using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EasyAccess.Infrastructure.Repository
{
    public interface IRepositoryBase<TEntity> : IRepository where TEntity : class
    {
        IQueryable<TEntity> Entities { get; }
        TEntity FindById(params object[] id);
        TEntity this[params object[] id] { get; }
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(params object[] id);
    }
}
