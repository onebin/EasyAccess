using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using EasyAccess.Infrastructure.Entity;
using EasyAccess.Infrastructure.UnitOfWork;

namespace EasyAccess.Infrastructure.Repository
{
    public abstract class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TKey>
    {

        public virtual IUnitOfWork UnitOfWork { get; set; }


        protected UnitOfWorkContextBase UnitOfWorkContext
        {
            get
            {
                if (UnitOfWork is UnitOfWorkContextBase)
                {
                    return UnitOfWork as UnitOfWorkContextBase;
                }
                throw new InvalidDataException("注入类型必须继承UnitOfWorkContextBase");
            }
        }

        public TEntity GetById(TKey id)
        {
            return UnitOfWorkContext.Set<TEntity, TKey>().Find(id);
        }

        public IQueryable<TEntity> Entities
        {
            get { return UnitOfWorkContext.Set<TEntity, TKey>(); }
        }

        public int Insert(TEntity entity, bool isSave = true)
        {
            UnitOfWorkContext.RegisterNew<TEntity, TKey>(entity);
            return isSave ? UnitOfWorkContext.Commit() : 0;
        }

        public int Insert(IEnumerable<TEntity> entities, bool isSave = true)
        {
            UnitOfWorkContext.RegisterNew<TEntity, TKey>(entities);
            return isSave ? UnitOfWorkContext.Commit() : 0;
        }

        public int Delete(TKey id, bool isSave = true)
        {
            var entity = UnitOfWorkContext.Set<TEntity, TKey>().Find(id);
            return entity != null ? Delete(entity, isSave) : 0;
        }

        public int Delete(TEntity entity, bool isSave = true)
        {
            UnitOfWorkContext.RegisterDeleted<TEntity, TKey>(entity);
            return isSave ? UnitOfWorkContext.Commit() : 0;
        }

        public int Delete(IEnumerable<TEntity> entities, bool isSave = true)
        {
            UnitOfWorkContext.RegisterDeleted<TEntity, TKey>(entities);
            return isSave ? UnitOfWorkContext.Commit() : 0;
        }

        public int Delete(Expression<Func<TEntity, bool>> predicate, bool isSave = true)
        {
            var entities = UnitOfWorkContext.Set<TEntity, TKey>().Where(predicate).ToList();
            return entities.Count > 0 ? Delete(entities, isSave) : 0;
        }

        public int Update(TEntity entity, bool isSave = true)
        {
            UnitOfWorkContext.RegisterModified<TEntity, TKey>(entity);
            return isSave ? UnitOfWorkContext.Commit() : 0;
        }


        public int Update(Expression<Func<TEntity, object>> propertyExpression, TEntity entity, bool isSave = true)
        {
            UnitOfWorkContext.RegisterModified<TEntity, TKey>(propertyExpression, entity);
            if (!isSave) return 0;
            var dbSet = UnitOfWorkContext.Set<TEntity, TKey>();
            dbSet.Local.Clear();
            var entry = UnitOfWorkContext.Entry<TEntity, TKey>(entity);
            return UnitOfWorkContext.Commit();
        }
    }
}
