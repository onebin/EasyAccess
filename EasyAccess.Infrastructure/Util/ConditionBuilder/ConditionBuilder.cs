using System;
using System.Collections.Generic;
using System.Linq;
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
        public Expression<Func<TEntity, bool>> Predicate
        {
            get
            {
                if (Parameters == null || Parameters.Length == 0)
                {
                    return Empty;
                }
                var expression = _expressions.Aggregate(Expression.AndAlso);
                return Expression.Lambda<Func<TEntity, bool>>(expression, Parameters);
            }
        }

        private readonly List<Expression> _expressions = new List<Expression>();
        private ParameterExpression[] Parameters { get; set; }

        /// <summary>
        /// 查询条件为空
        /// </summary>
        public static Expression<Func<TEntity, bool>> Empty
        {
            get
            {
                return x => true;
            }
        }

        public ConditionBuilder()
        {
        }

        public ConditionBuilder(Expression<Func<TEntity, bool>> expr)
        {
            _expressions.Add(expr);
        }

        private Expression GetMemberExpression<TType>(Expression<Func<TEntity, TType>> property)
        {
            if (Parameters == null || Parameters.Length == 0)
            {
                Parameters = property.Parameters.ToArray();
                return property.Body;
            }
            var visitor = new ParameterExpressionVisitor(this.Parameters[0]);
            return visitor.ChangeParameter(property.Body);
        }

        public ConditionBuilder<TEntity> Equals<TPropertyType>(Expression<Func<TEntity, TPropertyType>> property, TPropertyType value)
        {
            var left = GetMemberExpression(property);
            var right = Expression.Constant(value);
            _expressions.Add(Expression.Equal(left, right));
            return this;
        }
    }
}
