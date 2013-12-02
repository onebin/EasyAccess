using EasyAccess.Infrastructure.Repository;
using Spring.Context.Support;

namespace EasyAccess.Infrastructure.Entity
{
    public abstract class AggregateRootBase<TEntity, TKey> : AggregateBase<TKey>, IAggregateRootBase<TKey> where TEntity: class , IAggregateRoot
    {
        public static IRepositoryBase<TEntity> Repository
        {
            get { return ContextRegistry.GetContext().GetObject<IRepositoryBase<TEntity>>(); }
        }
    }
}
