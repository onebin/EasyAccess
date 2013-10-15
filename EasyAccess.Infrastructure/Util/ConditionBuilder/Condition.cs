using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
                    return ConditionBuilder<TEntity>.Empty;
                }
                var expression = _expressions.Aggregate(Expression.AndAlso);
                return Expression.Lambda<Func<TEntity, bool>>(expression, Parameters);
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

        private string GetBaseDataTypeValue<TProperty>(TProperty value)
        {
            Type type = typeof (TProperty).GetNonNullableType();
            if (type.IsBaseDataType())
            {
                return value.ToString();
            }
            return null;
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
            _expressions.Add(Expression.NotEqual(left, right));
            return this;
        }

        ICondition<TEntity> ICondition<TEntity>.Like<TProperty>(Expression<Func<TEntity, TProperty>> property, TProperty value)
        {
            var strVal = GetBaseDataTypeValue(value);
            if (string.IsNullOrWhiteSpace(strVal))
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
            throw new NotImplementedException();
        }

        ICondition<TEntity> ICondition<TEntity>.In<TProperty>(Expression<Func<TEntity, TProperty>> property, params TProperty[] values)
        {
            if (values != null && values.Length > 0)
            {
                var type = typeof (TProperty);
                var method = (from x in typeof (Enumerable).GetMethods()
                              where x.Name.Equals("Contains")
                                    && x.IsGenericMethod
                                    && x.GetGenericArguments().Length == 1
                                    && x.GetParameters().Length == 2
                              select x
                             ).First().MakeGenericMethod(new Type[] {type});

                var propertyBody = GetMemberExpression(property);

                var methodExpr = Expression.Call(null, method, Expression.Constant(values), propertyBody);
                _expressions.Add(methodExpr);
            }
            return this;
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


        ICondition<TEntity> ICondition<TEntity>.Fuzzy<TProperty>(Expression<Func<TEntity, TProperty>> property, string values)
        {
            if (!string.IsNullOrWhiteSpace(values))
            {
                values = values.Trim();
                if (values.Contains(","))
                {
                    var valArray = values.Split(',');
                    return ((ICondition<TEntity>)this).In(property, valArray.Where(x => !string.IsNullOrWhiteSpace(values)).Cast<TProperty>().ToArray());
                }
                if (values.Contains("-"))
                {
                    var dividerIndex = values.IndexOf('-');
                    if (dividerIndex > 0 && dividerIndex < values.Length -1)
                    {
                        var from = values.Substring(0, dividerIndex).Cast<TProperty>().FirstOrDefault();
                        var to = values.Substring(dividerIndex + 1).Cast<TProperty>().FirstOrDefault();
                        return ((ICondition<TEntity>)this).Between(property, from, to);
                    }
                }
            }
            return this;
        }
    }
}
