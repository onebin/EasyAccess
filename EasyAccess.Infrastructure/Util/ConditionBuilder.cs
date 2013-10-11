using System;
using System.Linq.Expressions;
using EasyAccess.Infrastructure.Entity;

namespace EasyAccess.Infrastructure.Util
{
    public class ConditionBuilder<TEnitty> where TEnitty : IEntity
    {
        private Expression<Func<TEnitty, bool>> _condition;

        public ConditionBuilder()
        {
        }

        public ConditionBuilder(Expression<Func<TEnitty, bool>> condition)
        {
            _condition = condition;
        }


        public ConditionBuilder<TEnitty> And(Expression<Func<TEnitty, bool>> expr)
        {
            return this;
        }

        public ConditionBuilder<TEnitty> Or(Expression<Func<TEnitty, bool>> expr)
        {
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


        public Expression<Func<TEnitty, bool>> ToPredicate()
        {
            return _condition;
        }
    }
}
