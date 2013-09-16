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
        private readonly DbContext _dbContext;

        protected RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<TEntity> LoadAll()
        {
            return _dbContext.Set<TEntity>();
        }

        public TEntity FindById(params object[] id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public void Insert(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbContext.Set<TEntity>().Attach(entity);
            }
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void Delete(params object[] id)
        {
            var removeItem = this.FindById(id);
            if (removeItem != null)
            {
                _dbContext.Set<TEntity>().Remove(removeItem);
            }
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
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
            return _dbContext.Set<TEntity>().Where(predicate);
        }
    }
}
