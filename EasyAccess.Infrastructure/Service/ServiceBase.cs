using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyAccess.Infrastructure.Entity;
using EasyAccess.Infrastructure.UnitOfWork;
using EasyAccess.Infrastructure.Util.ConditionBuilder;
using EasyAccess.Infrastructure.Util.PagingData;

namespace EasyAccess.Infrastructure.Service
{
    public abstract class ServiceBase
    {
        protected IUnitOfWork UnitOfWork { get; set; }

        protected PagingData<TEntity> GetPagingData<TEntity>(
            IQueryable<TEntity> entities,
            IQueryCondition<TEntity> queryCondition,
            PagingCondition pagingCondition) where TEntity : IAggregateRoot
        {
            var query = entities.Where(queryCondition.Predicate);
            var recordCount = query.Count();

            IOrderedQueryable<TEntity> orderCondition = null;
            if (queryCondition.KeySelectors == null || queryCondition.KeySelectors.Count == 0)
            {

            }
            else
            {
                
            }
            query = orderCondition;
            var recordData = query.Skip(pagingCondition.Skip).Take(pagingCondition.PageSize);
            var pageData = new PagingData<TEntity>(recordCount, pagingCondition, recordData);
            return pageData;
        }
    }
}
