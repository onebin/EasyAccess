using System;
using System.Linq.Expressions;
using System.Web.UI;
using EasyAccess.Infrastructure.Entity;

namespace EasyAccess.Infrastructure.Util.ConditionBuilder
{
    public class ConditionBuilder<TEntity> where TEntity : IAggregateRoot
    {
        /// <summary>
        /// 查询条件
        /// </summary>
        public Expression<Func<TEntity, bool>> Predicate { get; private set; }

        /// <summary>
        /// 查询条件为空
        /// </summary>
        public static Expression<Func<TEntity, bool>> Empty
        {
            get
            {
                var entity = Expression.Parameter(typeof(TEntity), "Entity");
                var expr = Expression.Equal(Expression.Constant(true, typeof(bool)), Expression.Constant(true, typeof(bool)));
                return Expression.Lambda<Func<TEntity, bool>>(expr, entity);
            }
        }

        public ConditionBuilder()
        {
            this.Predicate = Empty;
        }

        public ConditionBuilder(Expression<Func<TEntity, bool>> expr)
        {
            this.Predicate = expr;
        }


        public ConditionBuilder<TEntity> And(Expression<Func<TEntity, bool>> expr)
        {
            var condition = Expression.Lambda<Func<TEntity, bool>>(expr.Body, expr.Parameters);
            return this;
        }

        public ConditionBuilder<TEntity> Or(Expression<Func<TEntity, bool>> expr)
        {
            var condition = Expression.Lambda<Func<TEntity, bool>>(expr.Body, expr.Parameters);
            return this;
        }

        public static ConditionBuilder<TEntity> operator &(ConditionBuilder<TEntity> builder, Expression<Func<TEntity, bool>> expr)
        {
            return builder;
        }

        public static ConditionBuilder<TEntity> operator |(ConditionBuilder<TEntity> builder, Expression<Func<TEntity, bool>> expr)
        {
            return builder;
        }
    }
}
