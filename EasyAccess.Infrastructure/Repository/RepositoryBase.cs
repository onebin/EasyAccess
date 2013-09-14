using System.Collections.Generic;
using System.Data;
using System.Data.Entity;

namespace EasyAccess.Infrastructure.Repository
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly DbContext _dbContext;

        protected RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
        }

        public TEntity GetById(params object[] id)
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
            var removeItem = this.GetById(id);
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
    }
}
