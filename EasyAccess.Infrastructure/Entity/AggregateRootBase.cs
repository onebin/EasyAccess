using System.Collections.Generic;
using EasyAccess.Infrastructure.Repository;
using EasyAccess.Infrastructure.Util.ConditionBuilder;
using EasyAccess.Infrastructure.Util.PagingData;
using Spring.Context.Support;

namespace EasyAccess.Infrastructure.Entity
{
    public abstract class AggregateRootBase<TEntity, TKey> : AggregateBase<TKey>, IAggregateRootBase<TKey> where TEntity: class , IAggregateRoot
    {
        public static RepositoryBase<TEntity> Repository
        {
            get { return ContextRegistry.GetContext().GetObject<RepositoryBase<TEntity>>(); }
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
    }
}
