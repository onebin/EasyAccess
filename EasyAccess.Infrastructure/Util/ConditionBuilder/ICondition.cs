using System;
using System.Linq.Expressions;
using EasyAccess.Infrastructure.Entity;

namespace EasyAccess.Infrastructure.Util.ConditionBuilder
{
    public interface ICondition<TEntity> where TEntity : IAggregateRoot
    {
        Expression<Func<TEntity, bool>> Predicate { get; }

        Expression<Func<TEntity, bool>> Empty { get; }

        ICondition<TEntity> Equals<TProperty>(
            Expression<Func<TEntity, TProperty>> property,
            TProperty value);
        ICondition<TEntity> NotEquals<TProperty>(
            Expression<Func<TEntity, TProperty>> property,
            TProperty value);

        ICondition<TEntity> Like<TProperty>(
            Expression<Func<TEntity, TProperty>> property, 
            TProperty value);

        ICondition<TEntity> Between<TProperty>(
            Expression<Func<TEntity, TProperty>> property, 
            TProperty from,
            TProperty to);

        ICondition<TEntity> In<TProperty>(
            Expression<Func<TEntity, TProperty>> property,
            params TProperty[] values
            );

        ICondition<TEntity> NotIn<TProperty>(
            Expression<Func<TEntity, TProperty>> property,
            params TProperty[] values
            );

        ICondition<TEntity> GreaterThan<TProperty>(
            Expression<Func<TEntity, TProperty>> property,
            TProperty value);


        ICondition<TEntity> LessThan<TProperty>(
            Expression<Func<TEntity, TProperty>> property,
            TProperty value);
    }
}
