﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
using EasyAccess.Infrastructure.Entity;
using EasyAccess.Infrastructure.Repository;
using EasyAccess.Infrastructure.UnitOfWork;
using EasyAccess.Infrastructure.Util.ConditionBuilder;
using EasyAccess.Infrastructure.Util.PagingData;

namespace EasyAccess.Infrastructure.Service
{
    public abstract class ServiceBase
    {
        protected IUnitOfWork UnitOfWork { get; set; }

        protected PagingData<TEntity> GetPagingEntityDataModels<TEntity>(
            IRepositoryBase<TEntity> repository,
            IQueryCondition<TEntity> queryCondition,
            PagingCondition pagingCondition)
            where TEntity : class, IAggregateRoot
        {
            long recordCount;
            List<TEntity> recordData;
            repository.GetPagingEntityDataModels(queryCondition, pagingCondition, out recordData, out recordCount);
            return new PagingData<TEntity>(recordCount, pagingCondition, recordData);
        }

        protected PagingData<TDto> GetPagingDataTransferObjects<TDto,TEntity>(
            IRepositoryBase<TEntity> repository,
            IQueryCondition<TEntity> queryCondition,
            PagingCondition pagingCondition)
            where TEntity : class, IAggregateRoot
            where TDto : class 
        {
            long recordCount;
            List<TDto> recordData;
            repository.GetPagingDataTransferObjects(queryCondition, pagingCondition, out recordData, out recordCount);
            return new PagingData<TDto>(recordCount, pagingCondition, recordData);
        }
    }
}
