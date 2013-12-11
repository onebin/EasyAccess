using System;
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

        TEntity this[object id, bool getDeletedItem = false] { get; }

        TEntity GetById(object id, bool getDeletedItem = false);

        Expression<Func<TEntity, bool>> GetSofeDeletedExpr();

        void GetPagingEdmData(PagingCondition pagingCondition, out List<TEntity> recordData, out long recordCount, IQueryCondition<TEntity> queryCondition = null);

        void GetPagingDtoData<TDto>(PagingCondition pagingCondition, out List<TDto> recordData, out long recordCount,IQueryCondition<TEntity> queryCondition = null);

        int Insert(TEntity entity, bool isSave = true);

        int Insert(IEnumerable<TEntity> entities, bool isSave = true);

        int Delete(object id, bool isSave = true);

        int Delete(TEntity entity, bool isSave = true);

        int Delete(IEnumerable<TEntity> entities, bool isSave = true);

        int Delete(Expression<Func<TEntity, bool>> predicate, bool isSave = true);

        int Update(TEntity entity, bool isSave = true);

        int Update(Expression<Func<TEntity, object>> propertyExpression, bool isSave = true, params TEntity[] entities);

    }
}
