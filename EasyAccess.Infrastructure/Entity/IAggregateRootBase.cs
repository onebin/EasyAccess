namespace EasyAccess.Infrastructure.Entity
{
    public interface IAggregateRootBase<TKey> : IAggregateBase<TKey>, IAggregateRoot
    {
    }
}
