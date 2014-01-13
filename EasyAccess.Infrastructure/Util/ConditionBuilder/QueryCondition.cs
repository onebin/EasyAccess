using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using EasyAccess.Infrastructure.Entity;
using EasyAccess.Infrastructure.Extensions;

namespace EasyAccess.Infrastructure.Util.ConditionBuilder
{

    internal class QueryCondition<TEntity> : IQueryCondition<TEntity> where TEntity : IAggregateRoot
    {
        class CompareExpressions
        {
            public Expression Left { get; set; }

            public ConstantExpression Right { get; set; }
        }

        private readonly List<Expression> _expressions = new List<Expression>();
        private ParameterExpression[] Parameters { get; set; }

        bool IQueryCondition<TEntity>.IsGetSoftDeletedItems { get; set; }

        Expression<Func<TEntity, bool>> IQueryCondition<TEntity>.Predicate
        {
            get
            {
                if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
                {
                    if (!((IQueryCondition<TEntity>)this).IsGetSoftDeletedItems)
                    {
                        if (Parameters == null || Parameters.Length == 0)
                        {
                            Parameters = new[] { Expression.Parameter(typeof(TEntity)) };
                        }
                        var propExpr = Expression.Property(Parameters[0], "IsDeleted");
                        var constExpr = Expression.Constant(false);
                        _expressions.Add(Expression.Equal(propExpr, constExpr));
                    }
                }
                if (Parameters == null || Parameters.Length == 0)
                {
                    return ConditionBuilder<TEntity>.Empty;
                }
                var expression = _expressions.Aggregate(Expression.AndAlso);
                return Expression.Lambda<Func<TEntity, bool>>(expression, Parameters);
            }
        }

        Dictionary<string, OrderByCondition> IQueryCondition<TEntity>.OrderByConditions { get; set; }

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

        private CompareExpressions GetCompareExpressions<TProperty>(Expression<Func<TEntity, TProperty>> property, TProperty value)
        {
            var expr = new CompareExpressions();
            var propertyBody = GetMemberExpression(property);
            var constExpr = Expression.Constant(value);
            if (typeof(TProperty).IsNullableType())
            {
                propertyBody = Expression.Convert(propertyBody, typeof(TProperty).GetNonNullableType());
            }
            expr.Left = propertyBody;
            expr.Right = constExpr;
            return expr;
        }

        private string GetBaseDataTypeValue<TProperty>(TProperty value)
        {
            var type = typeof(TProperty).GetNonNullableType();
            return type.IsBasic() ? value.ToString() : null;
        }

        void IQueryCondition<TEntity>.Clear()
        {
            Parameters = null;
            _expressions.Clear();
            ((IQueryCondition<TEntity>) this).OrderByConditions = null;
        }

        bool IQueryCondition<TEntity>.IsEmpty()
        {
            return _expressions.Count == 0;
        }

        IQueryCondition<TEntity> IQueryCondition<TEntity>.Equal<TProperty>(Expression<Func<TEntity, TProperty>> property, params TProperty[] values)
        {
            _expressions.Add(values
                .Select(value => GetCompareExpressions(property, value))
                .Select(expr => Expression.Equal(expr.Left, expr.Right))
                .Aggregate(Expression.OrElse));
            return this;
        }
        IQueryCondition<TEntity> IQueryCondition<TEntity>.NotEqual<TProperty>(Expression<Func<TEntity, TProperty>> property, params TProperty[] values)
        {
            _expressions.Add(values
                .Select(value => GetCompareExpressions(property, value))
                .Select(expr => Expression.NotEqual(expr.Left, expr.Right))
                .Aggregate(Expression.OrElse));
            return this;
        }

        IQueryCondition<TEntity> IQueryCondition<TEntity>.Like<TProperty>(Expression<Func<TEntity, TProperty>> property, TProperty value)
        {
            var likeExpr = GetLikeExpr(property, value);
            if (likeExpr != null)
            {
                _expressions.Add(likeExpr);
            }
            return this;
        }


        IQueryCondition<TEntity> IQueryCondition<TEntity>.Between<TProperty>(Expression<Func<TEntity, TProperty>> property, TProperty from, TProperty to)
        {
            var propertyBody = GetMemberExpression(property);
            var constFrom = Expression.Constant(from);
            var constTo = Expression.Constant(to);
            if (typeof(TProperty).IsNullableType())
            {
                propertyBody = Expression.Convert(propertyBody, typeof(TProperty).GetNonNullableType());
            }
            var gteExpr = Expression.GreaterThanOrEqual(propertyBody, constFrom);
            var lteExpr = Expression.LessThanOrEqual(propertyBody, constTo);
            var btwExpr = Expression.AndAlso(gteExpr, lteExpr);
            _expressions.Add(btwExpr);
            return this;
        }

        IQueryCondition<TEntity> IQueryCondition<TEntity>.In<TProperty>(Expression<Func<TEntity, TProperty>> property, params TProperty[] values)
        {
            if (values != null && values.Length > 0)
            {
                var type = typeof(TProperty);
                var method = (from x in typeof(Enumerable).GetMethods()
                              where x.Name.Equals("Contains")
                                    && x.IsGenericMethod
                                    && x.GetGenericArguments().Length == 1
                                    && x.GetParameters().Length == 2
                              select x
                             ).First().MakeGenericMethod(new Type[] { type });

                var propertyBody = GetMemberExpression(property);

                var methodExpr = Expression.Call(null, method, Expression.Constant(values), propertyBody);
                _expressions.Add(methodExpr);
            }
            return this;
        }

        IQueryCondition<TEntity> IQueryCondition<TEntity>.NotIn<TProperty>(Expression<Func<TEntity, TProperty>> property, params TProperty[] values)
        {
            return ((IQueryCondition<TEntity>) this).NotEqual(property, values);
        }

        IQueryCondition<TEntity> IQueryCondition<TEntity>.GreaterThanOrEqual<TProperty>(Expression<Func<TEntity, TProperty>> property, TProperty value)
        {
            var expr = GetCompareExpressions(property, value);
            var gteExpr = Expression.GreaterThanOrEqual(expr.Left, expr.Right);
            _expressions.Add(gteExpr);
            return this;
        }

        IQueryCondition<TEntity> IQueryCondition<TEntity>.LessThanOrEqual<TProperty>(Expression<Func<TEntity, TProperty>> property, TProperty value)
        {
            var expr = GetCompareExpressions(property, value);
            var lteExpr = Expression.LessThanOrEqual(expr.Left, expr.Right);
            _expressions.Add(lteExpr);
            return this;
        }

        IQueryCondition<TEntity> IQueryCondition<TEntity>.GreaterThan<TProperty>(Expression<Func<TEntity, TProperty>> property, TProperty value)
        {
            var expr = GetCompareExpressions(property, value);
            var lteExpr = Expression.GreaterThan(expr.Left, expr.Right);
            _expressions.Add(lteExpr);
            return this;
        }

        IQueryCondition<TEntity> IQueryCondition<TEntity>.LessThan<TProperty>(Expression<Func<TEntity, TProperty>> property, TProperty value)
        {
            var expr = GetCompareExpressions(property, value);
            var lteExpr = Expression.LessThan(expr.Left, expr.Right);
            _expressions.Add(lteExpr);
            return this;
        }

        IQueryCondition<TEntity> IQueryCondition<TEntity>.Fuzzy<TProperty>(Expression<Func<TEntity, TProperty>> property, string values)
        {
            if (!string.IsNullOrWhiteSpace(values))
            {
                var keywords = values.Trim().Split(new[] { ',', '，', ';', '；' });
                if (keywords.Count() > 1)
                {
                    var exprs = keywords
                              .Select(keyword => GetLikeExpr(property, (TProperty)Convert.ChangeType(keyword.Trim(), typeof(TProperty))))
                              .Where(likeExpr => likeExpr != null)
                              .Cast<Expression>()
                              .ToList();
                    var expr = exprs.Aggregate(Expression.OrElse);
                    _expressions.Add(expr);
                }
                else
                {
                    keywords = values.Split(new[] { ' ', '\t' });
                    foreach (var likeExpr in keywords
                        .Select(keyword => GetLikeExpr(property, (TProperty)Convert.ChangeType(keyword.Trim(), typeof(TProperty))))
                        .Where(likeExpr => likeExpr != null))
                    {
                        _expressions.Add(likeExpr);
                    }
                }
            }
            return this;
        }

        ThenByCondition<TEntity> IQueryCondition<TEntity>.OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return Sort(keySelector, ListSortDirection.Ascending);
        }

        ThenByCondition<TEntity> IQueryCondition<TEntity>.OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return Sort(keySelector, ListSortDirection.Descending);
        }

        private ThenByCondition<TEntity> Sort<TKey>(Expression<Func<TEntity, TKey>> keySelector, ListSortDirection direction)
        {
            var keyName = GetPropertyName(keySelector);
            if (!string.IsNullOrEmpty(keyName))
            {
                InitOrderByCondition();
                RemoveOrderByConditionItem(keyName);
                AddOrderByConditionItem(keyName, keySelector, direction);
            }
            return new ThenByCondition<TEntity>((IQueryCondition<TEntity>)this);
        }

        private string GetPropertyName<TKey>(Expression<Func<TEntity, TKey>> expr)
        {
            var name = string.Empty;
            if (expr.Body is MemberExpression)
            {
                name = ((MemberExpression)expr.Body).Member.Name;
            }
            else if (expr.Body is NewExpression)
            {
                throw new NotSupportedException("暂不支持以new{}方式传递参数");
            }
            return name;
        }

        private void InitOrderByCondition()
        {
            if (((IQueryCondition<TEntity>)this).OrderByConditions == null)
            {
                ((IQueryCondition<TEntity>)this).OrderByConditions = new Dictionary<string, OrderByCondition>();
            }
        }

        private void RemoveOrderByConditionItem(string keyName)
        {
            if (((IQueryCondition<TEntity>)this).OrderByConditions.ContainsKey(keyName))
            {
                ((IQueryCondition<TEntity>)this).OrderByConditions.Remove(keyName);
            }
        }

        private void AddOrderByConditionItem<TKey>(string keyName, Expression<Func<TEntity, TKey>> keySelector, ListSortDirection direction)
        {

            ((IQueryCondition<TEntity>)this).OrderByConditions.Add(
                keyName,
                new OrderByCondition
                {
                    KeySelector = keySelector,
                    Direction = direction
                });
        }

        private MethodCallExpression GetLikeExpr<TProperty>(Expression<Func<TEntity, TProperty>> property, TProperty value)
        {
            var strVal = GetBaseDataTypeValue(value);
            if (!string.IsNullOrWhiteSpace(strVal))
            {
                var propertyBody = GetMemberExpression(property);
                var methodExpr = Expression.Call(
                    propertyBody,
                    typeof(string).GetMethod("Contains"),
                    Expression.Constant(strVal));
                return methodExpr;
            }
            return null;
        }
    }
}
