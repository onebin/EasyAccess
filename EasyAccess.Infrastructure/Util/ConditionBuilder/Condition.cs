using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using EasyAccess.Infrastructure.Entity;
using EasyAccess.Infrastructure.Extensions;

namespace EasyAccess.Infrastructure.Util.ConditionBuilder
{
    internal class Condition<TEntity> : ICondition<TEntity> where TEntity : IAggregateRoot
    {

        private readonly List<Expression> _expressions = new List<Expression>();
        private ParameterExpression[] Parameters { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        Expression<Func<TEntity, bool>> ICondition<TEntity>.Predicate
        {
            get
            {
                if (Parameters == null || Parameters.Length == 0)
                {
                    return ((ICondition<TEntity>)this).Empty;
                }
                var expression = _expressions.Aggregate(Expression.AndAlso);
                return Expression.Lambda<Func<TEntity, bool>>(expression, Parameters);
            }
        }

        /// <summary>
        /// 查询条件为空
        /// </summary>
        Expression<Func<TEntity, bool>> ICondition<TEntity>.Empty
        {
            get
            {
                return x => true;
            }
        }

        private Expression GetMemberExpression<TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            if (Parameters == null || Parameters.Length == 0)
            {
                Parameters = property.Parameters.ToArray();
                return property.Body;
            }
            var visitor = new ParameterExpressionVisitor(this.Parameters[0]);
            return visitor.ChangeParameter(property.Body);
        }

        ICondition<TEntity> ICondition<TEntity>.Equals<TProperty>(Expression<Func<TEntity, TProperty>> property, TProperty value)
        {
            var left = GetMemberExpression(property);
            var right = Expression.Constant(value, typeof(TProperty));
            _expressions.Add(Expression.Equal(left, right));
            return this;
        }
        ICondition<TEntity> ICondition<TEntity>.NotEquals<TProperty>(Expression<Func<TEntity, TProperty>> property, TProperty value)
        {
            var left = GetMemberExpression(property);
            var right = Expression.Constant(value, typeof(TProperty));
            _expressions.Add(Expression.Equal(left, right));
            throw new NotImplementedException();
        }

        ICondition<TEntity> ICondition<TEntity>.Like<TProperty>(Expression<Func<TEntity, TProperty>> property, TProperty value)
        {
            var strVal = value.ToString().Trim();
            if (!string.IsNullOrEmpty(strVal))
            {
                var propertyBody = GetMemberExpression(property);
                var methodExpr = Expression.Call(
                    propertyBody, 
                    typeof (string).GetMethod("Contains"),
                    Expression.Constant(strVal));
                _expressions.Add(methodExpr);
            }
            return this;
        }


        ICondition<TEntity> ICondition<TEntity>.Between<TProperty>(Expression<Func<TEntity, TProperty>> property, TProperty from, TProperty to)
        {
            var strFrom = from.ToString().Trim();
            var strTo = from.ToString().Trim();
            if (!string.IsNullOrEmpty(strFrom) && !string.IsNullOrWhiteSpace(strTo))
            {
                
            }
            return this;
        }

        ICondition<TEntity> ICondition<TEntity>.In<TProperty>(Expression<Func<TEntity, TProperty>> property, params TProperty[] values)
        {
            throw new NotImplementedException();
        }

        ICondition<TEntity> ICondition<TEntity>.NotIn<TProperty>(Expression<Func<TEntity, TProperty>> property, params TProperty[] values)
        {
            throw new NotImplementedException();
        }

        ICondition<TEntity> ICondition<TEntity>.GreaterThan<TProperty>(Expression<Func<TEntity, TProperty>> property, TProperty value)
        {
            throw new NotImplementedException();
        }

        ICondition<TEntity> ICondition<TEntity>.LessThan<TProperty>(Expression<Func<TEntity, TProperty>> property, TProperty value)
        {
            throw new NotImplementedException();
        }
    }
}
