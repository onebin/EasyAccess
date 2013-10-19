using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
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
            PagingCondition pagingCondition) where TEntity : class, IAggregateRoot
        {
            var query = entities.Where(queryCondition.Predicate);
            var recordCount = query.Count();

            IOrderedQueryable<TEntity> orderCondition = null;
            if (queryCondition.OrderByConditions == null || queryCondition.OrderByConditions.Count == 0)
            {
                orderCondition = entities.OrderBy(x => x.Id);
            }
            else
            {
                var i = 0;
                foreach (var keySelector in queryCondition.OrderByConditions)
                {
                    orderCondition = i == 0
                        ? keySelector.Value.Direction == ListSortDirection.Ascending
                            ? Queryable.OrderBy(entities, (dynamic)keySelector.Value.KeySelector)
                            : Queryable.OrderByDescending(entities, (dynamic)keySelector.Value.KeySelector)
                        : keySelector.Value.Direction == ListSortDirection.Ascending
                            ? Queryable.ThenBy(orderCondition, (dynamic)keySelector.Value.KeySelector)
                            : Queryable.ThenByDescending(orderCondition, (dynamic)keySelector.Value.KeySelector);

                    i++;
                }
            }
            query = orderCondition;
            var recordData = query.Skip(pagingCondition.Skip).Take(pagingCondition.PageSize);
            var pageData = new PagingData<TEntity>(recordCount, pagingCondition, recordData);
            return pageData;
        }
    }
}
