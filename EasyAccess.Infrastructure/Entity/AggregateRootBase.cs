﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EasyAccess.Infrastructure.Repository;
using EasyAccess.Infrastructure.Util.ConditionBuilder;
using EasyAccess.Infrastructure.Util.PagingData;
using Spring.Context.Support;

namespace EasyAccess.Infrastructure.Entity
{
    public abstract class AggregateRootBase<TEntity, TKey> : AggregateBase<TKey>, IAggregateRootBase<TEntity, TKey> where TEntity: class , IAggregateRoot
    {
        protected static RepositoryBase<TEntity> Repository
        {
            get { return ContextRegistry.GetContext().GetObject<RepositoryBase<TEntity>>(); }
        }

        public static TEntity FindById(object id)
        {
            return Repository[id];
        }

        public static IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expr)
        {
            return Repository.Entities.Where(expr);
        }

        public static IQueryable<TEntity> FindAll()
        {
            return Repository.Entities;
        }

        public static PagingData<TDto> GetPagingDtoData<TDto>(
            PagingCondition pagingCondition,
            IQueryCondition<TEntity> queryCondition = null)
            where TDto : class
        {
            long recordCount;
            List<TDto> recordData;
            Repository.GetPagingDtoData(pagingCondition, out recordData, out recordCount, queryCondition);
            return new PagingData<TDto>(recordCount, pagingCondition, recordData);
        }

        public static PagingData<TEntity> GetPagingEdmData(
            PagingCondition pagingCondition,
            IQueryCondition<TEntity> queryCondition = null)
        {
            long recordCount;
            List<TEntity> recordData;
            Repository.GetPagingEdmData(pagingCondition, out recordData, out recordCount, queryCondition);
            return new PagingData<TEntity>(recordCount, pagingCondition, recordData);
        }

        public static void Insert(TEntity entity)
        {
            Repository.Insert(entity, false);
        }

        public static void Insert(IEnumerable<TEntity> entities)
        {
            Repository.Insert(entities, false);
        }

        public static void Delete(object id)
        {
            Repository.Delete(id, false);
        }

        public static void Delete(TEntity entity)
        {
            Repository.Delete(entity, false);
        }

        public static void Delete(IEnumerable<TEntity> entities)
        {
            Repository.Delete(entities, false);
        }

        public static void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            Repository.Delete(predicate, false);
        }

        public static void Update(TEntity entity)
        {
            Repository.Update(entity, false);
        }

        public static void Update(Expression<Func<TEntity, object>> propertyExpression, params TEntity[] entities)
        {
            Repository.Update(propertyExpression, false, entities);
        }
    }
}
