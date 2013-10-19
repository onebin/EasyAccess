using System.Linq.Expressions;
using EasyAccess.Infrastructure.Entity;
using System;

namespace EasyAccess.Infrastructure.Util.ConditionBuilder
{
    public class ThenByCondition<TEntity> where TEntity : IAggregateRoot
    {
        private IQueryCondition<TEntity> QueryCondition { get; set; }

        public ThenByCondition(IQueryCondition<TEntity> queryCondition)
        {
            this.QueryCondition = queryCondition;
        }

        public ThenByCondition<TEntity> ThenBy<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return QueryCondition.OrderBy(keySelector);
        }

        public ThenByCondition<TEntity> ThenByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return QueryCondition.OrderByDescending(keySelector);
        }
    }
}
