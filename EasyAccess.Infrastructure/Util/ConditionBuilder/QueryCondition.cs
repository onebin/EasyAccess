using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
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

        Expression<Func<TEntity, bool>> IQueryCondition<TEntity>.Predicate
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

        Dictionary<string, ListSortDirection> IQueryCondition<TEntity>.KeySelectors { get; set; }

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
                propertyBody = Expression.Convert(propertyBody, typeof (TProperty).GetNonNullableType());
            }
            expr.Left = propertyBody;
            expr.Right = constExpr;
            return expr;
        }

        private string GetBaseDataTypeValue<TProperty>(TProperty value)
        {
            var type = typeof (TProperty).GetNonNullableType();
            return type.IsBaseDataType() ? value.ToString() : null;
        }

        void IQueryCondition<TEntity>.Clear()
        {
            Parameters = null;
            _expressions.Clear();
        }

        bool IQueryCondition<TEntity>.IsEmpty()
        {
            return _expressions.Count == 0;
        }

        IQueryCondition<TEntity> IQueryCondition<TEntity>.Equal<TProperty>(Expression<Func<TEntity, TProperty>> property, TProperty value)
        {
            var expr = GetCompareExpressions(property, value);
            _expressions.Add(Expression.Equal(expr.Left, expr.Right));
            return this;
        }
        IQueryCondition<TEntity> IQueryCondition<TEntity>.NotEqual<TProperty>(Expression<Func<TEntity, TProperty>> property, TProperty value)
        {
            var expr = GetCompareExpressions(property, value);
            _expressions.Add(Expression.NotEqual(expr.Left, expr.Right));
            return this;
        }

        IQueryCondition<TEntity> IQueryCondition<TEntity>.Like<TProperty>(Expression<Func<TEntity, TProperty>> property, TProperty value)
        {
            var strVal = GetBaseDataTypeValue(value);
            if (!string.IsNullOrWhiteSpace(strVal))
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


        IQueryCondition<TEntity> IQueryCondition<TEntity>.Between<TProperty>(Expression<Func<TEntity, TProperty>> property, TProperty from, TProperty to)
        {
            var propertyBody = GetMemberExpression(property);
            var constFrom = Expression.Constant(from);
            var constTo = Expression.Constant(to);
            if (typeof(TProperty).IsNullableType())
            {
                propertyBody = Expression.Convert(propertyBody, typeof (TProperty).GetNonNullableType());
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

        IQueryCondition<TEntity> IQueryCondition<TEntity>.NotIn<TProperty>(Expression<Func<TEntity, TProperty>> property, params TProperty[] values)
        {
            throw new NotImplementedException();
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
                values = values.Trim();
                if (values.Contains(","))
                {
                    var valArray = values.Split(',');
                    return ((IQueryCondition<TEntity>)this).In(property, valArray.Where(x => !string.IsNullOrWhiteSpace(values)).Cast<TProperty>().ToArray());
                }
                if (values.Contains("-"))
                {
                    var dividerIndex = values.IndexOf('-');
                    if (dividerIndex > 0 && dividerIndex < values.Length -1)
                    {
                        var from = values.Substring(0, dividerIndex).Cast<TProperty>().FirstOrDefault();
                        var to = values.Substring(dividerIndex + 1).Cast<TProperty>().FirstOrDefault();
                        return ((IQueryCondition<TEntity>)this).Between(property, from, to);
                    }
                }
            }
            return this;
        }

        void IQueryCondition<TEntity>.OrderBy<TKey>(ListSortDirection direction, params Expression<Func<TEntity, TKey>>[] keySelectors)
        {
            foreach (var keySelector in keySelectors)
            {
                ((IQueryCondition<TEntity>)this).OrderBy(keySelector, direction);
            }
        }

        void IQueryCondition<TEntity>.OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector, ListSortDirection direction)
        {
            var keyNames = GetPropertyNames(keySelector);
            if(!keyNames.Any()) return;
            foreach (var keyName in keyNames.Where(keyName => !string.IsNullOrEmpty(keyName)))
            {
                if (((IQueryCondition<TEntity>)this).KeySelectors == null)
                {
                    ((IQueryCondition<TEntity>)this).KeySelectors = new Dictionary<string, ListSortDirection>();
                }
                if (((IQueryCondition<TEntity>)this).KeySelectors.ContainsKey(keyName))
                {
                    ((IQueryCondition<TEntity>) this).KeySelectors[keyName] = direction;
                }
                else
                {
                    ((IQueryCondition<TEntity>)this).KeySelectors.Add(keyName, direction);
                }
            }
        }

        private List<string> GetPropertyNames<TEntity, TKey>(Expression<Func<TEntity, TKey>> expr)
        {
            var names = new List<string>();
            if (expr.Body is MemberExpression)
            {
                names.Add(((MemberExpression)expr.Body).Member.Name);
            }
            else if (expr.Body is NewExpression)
            {
                var members = ((NewExpression)expr.Body).Members;
                names.AddRange(members.Select(x => x.Name));
            }
            return names;
        }
    }
}
