namespace EasyAccess.Infrastructure.Entity
{
    public interface IAggregateRootBase<TEntity, TKey> : IAggregateBase<TKey>, IAggregateRoot
    {
    }
}
