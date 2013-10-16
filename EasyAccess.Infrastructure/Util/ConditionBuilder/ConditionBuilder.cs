using System;
using System.Linq.Expressions;
using EasyAccess.Infrastructure.Entity;

namespace EasyAccess.Infrastructure.Util.ConditionBuilder
{
    public static class ConditionBuilder<TEntity> where TEntity : IAggregateRoot
    {
        public static IQueryCondition<TEntity> Create()
        {
            return new QueryCondition<TEntity>();
        }

        /// <summary>
        /// 查询条件为空
        /// </summary>
        public static Expression<Func<TEntity, bool>> Empty
        {
            get { return x => true; }
        }
    }
}
