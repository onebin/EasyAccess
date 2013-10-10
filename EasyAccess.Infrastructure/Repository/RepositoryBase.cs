using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EasyAccess.Infrastructure.Entity;
using EasyAccess.Infrastructure.UnitOfWork;

namespace EasyAccess.Infrastructure.Repository
{
    public abstract class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>
        where TEntity : IAggregateRoot<TKey>
        where TKey : struct 
    {
        public IQueryable<TEntity> Entities
        {
            get { throw new NotImplementedException(); }
        }

        public int Insert(TEntity entity, bool isSave = true)
        {
            throw new NotImplementedException();
        }

        public int Insert(IEnumerable<TEntity> entities, bool isSave = true)
        {
            throw new NotImplementedException();
        }

        public int Delete(object id, bool isSave = true)
        {
            throw new NotImplementedException();
        }

        public int Delete(TEntity entity, bool isSave = true)
        {
            throw new NotImplementedException();
        }

        public int Delete(IEnumerable<TEntity> entities, bool isSave = true)
        {
            throw new NotImplementedException();
        }

        public int Delete(Expression<Func<TEntity, bool>> predicate, bool isSave = true)
        {
            throw new NotImplementedException();
        }

        public int Update(TEntity entity, bool isSave = true)
        {
            throw new NotImplementedException();
        }

        public TEntity GetByKey(object key)
        {
            throw new NotImplementedException();
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
