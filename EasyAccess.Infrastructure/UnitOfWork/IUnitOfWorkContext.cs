using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using EasyAccess.Infrastructure.Entity;

namespace EasyAccess.Infrastructure.UnitOfWork
{
    public interface IUnitOfWorkContext: IUnitOfWork
    {
        DbSet<TEntity> Set<TEntity>()
            where TEntity : class, IAggregateRoot;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity)
            where TEntity : class, IAggregateRoot;

        void RegisterNew<TEntity>(TEntity entity)
            where TEntity : class, IAggregateRoot;

        void RegisterNew<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class, IAggregateRoot;

        void RegisterModified<TEntity>(params TEntity[] entities)
            where TEntity : class, IAggregateRoot;

        void RegisterModified<TEntity>(Expression<Func<TEntity, object>> propertyExpression,
            params TEntity[] entities)
            where TEntity : class, IAggregateRoot;

        void RegisterDeleted<TEntity>(TEntity entity)
            where TEntity : class, IAggregateRoot;

        void RegisterDeleted<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class, IAggregateRoot;
    }
}
