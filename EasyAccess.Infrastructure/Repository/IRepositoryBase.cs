using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EasyAccess.Infrastructure.Repository
{
    public interface IRepositoryBase<TEntity> : IRepository where TEntity : class
    {
        IEnumerable<TEntity> LoadAll();
        TEntity FindById(params object[] id);
        TEntity this[params object[] id] { get; }
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(params object[] id);
        IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
    }
}
