namespace EasyAccess.Infrastructure.Entity
{
    public interface IAggregateBase<TKey> : IEntity
    {
        new TKey Id { get; set; }
    }
}
