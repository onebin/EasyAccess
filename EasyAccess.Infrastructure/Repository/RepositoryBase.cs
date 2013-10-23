﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper.QueryableExtensions;
using EasyAccess.Infrastructure.Entity;
using EasyAccess.Infrastructure.UnitOfWork;
using EasyAccess.Infrastructure.Util.ConditionBuilder;
using EasyAccess.Infrastructure.Util.PagingData;

namespace EasyAccess.Infrastructure.Repository
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : class, IAggregateRoot
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

        public IQueryable<TEntity> Entities
        {
            get { return UnitOfWorkContext.Set<TEntity>(); }
        }

        public TEntity GetById(object id)
        {
            return UnitOfWorkContext.Set<TEntity>().Find(id);
        }

        private IQueryable<TEntity> GetQueryableEntityFromConditon(
            IQueryCondition<TEntity> queryCondition,
            PagingCondition pagingCondition)
        {
            var query = Entities.Where(queryCondition.Predicate);
            IOrderedQueryable<TEntity> orderCondition = null;
            if (queryCondition.OrderByConditions == null || queryCondition.OrderByConditions.Count == 0)
            {
                orderCondition = Entities.OrderBy(x => x.Id);
            }
            else
            {
                var i = 0;
                foreach (var keySelector in queryCondition.OrderByConditions)
                {
                    orderCondition = i == 0
                        ? keySelector.Value.Direction == ListSortDirection.Ascending
                            ? Queryable.OrderBy(Entities, (dynamic)keySelector.Value.KeySelector)
                            : Queryable.OrderByDescending(Entities, (dynamic)keySelector.Value.KeySelector)
                        : keySelector.Value.Direction == ListSortDirection.Ascending
                            ? Queryable.ThenBy(orderCondition, (dynamic)keySelector.Value.KeySelector)
                            : Queryable.ThenByDescending(orderCondition, (dynamic)keySelector.Value.KeySelector);

                    i++;
                }
            }
            query = orderCondition;
            return query;
        }

        public void GetPagingEntityDataModels(IQueryCondition<TEntity> queryCondition, PagingCondition pagingCondition, out List<TEntity> recordData, out long recordCount)
        {
            var query = GetQueryableEntityFromConditon(queryCondition, pagingCondition);
            recordCount = query.Count();
            recordData = query.Skip(pagingCondition.Skip).Take(pagingCondition.PageSize).ToList();
        }

        public void GetPagingDataTransferObjects<TDto>(IQueryCondition<TEntity> queryCondition, PagingCondition pagingCondition, out List<TDto> recordData, out long recordCount)
        {
            var query = GetQueryableEntityFromConditon(queryCondition, pagingCondition);
            recordCount = query.Count();
            recordData = query.Skip(pagingCondition.Skip).Take(pagingCondition.PageSize).Project().To<TDto>().ToList();
        }

        public int Insert(TEntity entity, bool isSave = true)
        {
            UnitOfWorkContext.RegisterNew(entity);
            return isSave ? UnitOfWorkContext.Commit() : 0;
        }

        public int Insert(IEnumerable<TEntity> entities, bool isSave = true)
        {
            UnitOfWorkContext.RegisterNew(entities);
            return isSave ? UnitOfWorkContext.Commit() : 0;
        }

        public int Delete(object id, bool isSave = true)
        {
            var entity = UnitOfWorkContext.Set<TEntity>().Find(id);
            return entity != null ? Delete(entity, isSave) : 0;
        }

        public int Delete(TEntity entity, bool isSave = true)
        {
            UnitOfWorkContext.RegisterDeleted(entity);
            return isSave ? UnitOfWorkContext.Commit() : 0;
        }

        public int Delete(IEnumerable<TEntity> entities, bool isSave = true)
        {
            UnitOfWorkContext.RegisterDeleted(entities);
            return isSave ? UnitOfWorkContext.Commit() : 0;
        }

        public int Delete(Expression<Func<TEntity, bool>> predicate, bool isSave = true)
        {
            var entities = UnitOfWorkContext.Set<TEntity>().Where(predicate).ToList();
            return entities.Count > 0 ? Delete(entities, isSave) : 0;
        }

        public int Update(TEntity entity, bool isSave = true)
        {
            UnitOfWorkContext.RegisterModified(entity);
            return isSave ? UnitOfWorkContext.Commit() : 0;
        }


        public int Update(Expression<Func<TEntity, object>> propertyExpression, TEntity entity, bool isSave = true)
        {
            UnitOfWorkContext.RegisterModified(propertyExpression, entity);
            if (!isSave) return 0;
            var dbSet = UnitOfWorkContext.Set<TEntity>();
            dbSet.Local.Clear();
            UnitOfWorkContext.Entry(entity).State = EntityState.Modified;
            return UnitOfWorkContext.Commit();
        }
    }
}
