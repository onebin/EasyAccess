using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EasyAccess.Infrastructure.Entity
{
    public interface IAggregateRootBase<TEntity, TKey> : IAggregateBase<TKey>, IAggregateRoot
    {
        void Insert(TEntity entity);

        void Insert(IEnumerable<TEntity> entities);

        void Delete(object id);

        void Delete(TEntity entity);

        void Delete(IEnumerable<TEntity> entities);

        void Delete(Expression<Func<TEntity, bool>> predicate);

        void Update(TEntity entity);

        void Update(Expression<Func<TEntity, object>> propertyExpression, params TEntity[] entities);
    }
}
