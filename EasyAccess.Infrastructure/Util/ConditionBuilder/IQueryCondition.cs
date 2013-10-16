using System;
using System.Linq.Expressions;
using EasyAccess.Infrastructure.Entity;

namespace EasyAccess.Infrastructure.Util.ConditionBuilder
{
    public interface IQueryCondition<TEntity> where TEntity : IAggregateRoot
    {
        Expression<Func<TEntity, bool>> Predicate { get; }

        void Clear();

        bool IsEmpty();

        IQueryCondition<TEntity> Equal<TProperty>(
            Expression<Func<TEntity, TProperty>> property,
            TProperty value);

        IQueryCondition<TEntity> NotEqual<TProperty>(
            Expression<Func<TEntity, TProperty>> property,
            TProperty value);

        IQueryCondition<TEntity> GreaterThanOrEqual<TProperty>(
            Expression<Func<TEntity, TProperty>> property,
            TProperty value);

        IQueryCondition<TEntity> LessThanOrEqual<TProperty>(
            Expression<Func<TEntity, TProperty>> property,
            TProperty value);

        IQueryCondition<TEntity> GreaterThan<TProperty>(
            Expression<Func<TEntity, TProperty>> property,
            TProperty value);

        IQueryCondition<TEntity> LessThan<TProperty>(
            Expression<Func<TEntity, TProperty>> property,
            TProperty value);

        IQueryCondition<TEntity> Like<TProperty>(
            Expression<Func<TEntity, TProperty>> property, 
            TProperty value);

        IQueryCondition<TEntity> Between<TProperty>(
            Expression<Func<TEntity, TProperty>> property, 
            TProperty from,
            TProperty to);

        IQueryCondition<TEntity> In<TProperty>(
            Expression<Func<TEntity, TProperty>> property,
            params TProperty[] values);

        IQueryCondition<TEntity> NotIn<TProperty>(
            Expression<Func<TEntity, TProperty>> property,
            params TProperty[] values);

        IQueryCondition<TEntity> Fuzzy<TProperty>(
            Expression<Func<TEntity, TProperty>> property, 
            string values);
    }
}
