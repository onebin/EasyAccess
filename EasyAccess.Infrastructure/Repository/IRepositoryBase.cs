﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EasyAccess.Infrastructure.Entity;
using EasyAccess.Infrastructure.UnitOfWork;
using EasyAccess.Infrastructure.Util.ConditionBuilder;
using EasyAccess.Infrastructure.Util.PagingData;

namespace EasyAccess.Infrastructure.Repository
{
    public interface IRepositoryBase<TEntity> : IRepository 
        where TEntity : class, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; set; }

        IQueryable<TEntity> Entities { get; }

        TEntity this[object id] { get; }

        TEntity GetById(object id);

        void GetPagingEdmData(PagingCondition pagingCondition, out List<TEntity> recordData, out long recordCount, IQueryCondition<TEntity> queryCondition = null);

        void GetPagingDtoData<TDto>(PagingCondition pagingCondition, out List<TDto> recordData, out long recordCount,IQueryCondition<TEntity> queryCondition = null);

        int Insert(TEntity entity, bool isSave = false);

        int Insert(IEnumerable<TEntity> entities, bool isSave = false);

        int Delete(object id, bool isSave = false);

        int Delete(TEntity entity, bool isSave = false);

        int Delete(IEnumerable<TEntity> entities, bool isSave = false);

        int Delete(Expression<Func<TEntity, bool>> predicate, bool isSave = false);

        int Update(TEntity entity, bool isSave = false);

        int Update(Expression<Func<TEntity, object>> propertyExpression, bool isSave = false, params TEntity[] entities);

    }
}
