using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace EasyAccess.Infrastructure.Repository
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly DbContext DbContext;

        protected RepositoryBase(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public IEnumerable<TEntity> LoadAll()
        {
            return DbContext.Set<TEntity>();
        }

        public TEntity FindById(params object[] id)
        {
            return DbContext.Set<TEntity>().Find(id);
        }

        public void Insert(TEntity entity)
        {
            DbContext.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached)
            {
                DbContext.Set<TEntity>().Attach(entity);
            }
            DbContext.Set<TEntity>().Remove(entity);
        }

        public void Delete(params object[] id)
        {
            var removeItem = this.FindById(id);
            if (removeItem != null)
            {
                DbContext.Set<TEntity>().Remove(removeItem);
            }
        }

        public void Update(TEntity entity)
        {
            DbContext.Set<TEntity>().Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }


        public TEntity this[params object[] id]
        {
            get
            {
                return this.FindById(id);
            }
        }

        public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>>  predicate)
        {
            return DbContext.Set<TEntity>().Where(predicate);
        }
    }
}
