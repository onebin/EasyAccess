using System;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using EasyAccess.Infrastructure.Entity;

namespace EasyAccess.Infrastructure.Util.ConditionBuilder
{
    public static class ConditionBuilder
    {
        public static ICondition<TEntity> Create<TEntity>() where TEntity : IAggregateRoot
        {
            return new Condition<TEntity>();
        }
    }
}
