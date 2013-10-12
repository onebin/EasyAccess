using System;
using System.Linq.Expressions;
using EasyAccess.Infrastructure.Entity;

namespace EasyAccess.Infrastructure.Util.ConditionBuilder
{
    public class ConditionBuilder<TEnitty> where TEnitty : IEntity
    {
        public Expression<Func<TEnitty, bool>> Predicate { get; private set; }

        public ConditionBuilder()
        {
        }

        public ConditionBuilder(Expression<Func<TEnitty, bool>> expr)
        {
            Predicate = expr;
        }


        public ConditionBuilder<TEnitty> And(Expression<Func<TEnitty, bool>> expr)
        {
            var condition = Expression.Lambda<Func<TEnitty, bool>>(expr.Body, expr.Parameters);
            if (Predicate != null)
            {
                var invokedExpr = Expression.Invoke(expr, expr.Parameters);
                this.Predicate = Expression.Lambda<Func<TEnitty, bool>>(Expression.And(Predicate.Body, invokedExpr),
                                                                         condition.Parameters);
            }
            else
            {
                this.Predicate = condition;
            }
            return this;
        }

        public ConditionBuilder<TEnitty> Or(Expression<Func<TEnitty, bool>> expr)
        {
            var invokedExpr = Expression.Invoke(Predicate, expr.Parameters);
            this.Predicate = Expression.Lambda<Func<TEnitty, bool>>(Expression.Or(Predicate.Body, invokedExpr),
                                                                     expr.Parameters);
            return this;
        }

        public static ConditionBuilder<TEnitty> operator &(ConditionBuilder<TEnitty> builder, Expression<Func<TEnitty, bool>> expr)
        {
            return builder;
        }

        public static ConditionBuilder<TEnitty> operator |(ConditionBuilder<TEnitty> builder, Expression<Func<TEnitty, bool>> expr)
        {
            return builder;
        }
    }
}
