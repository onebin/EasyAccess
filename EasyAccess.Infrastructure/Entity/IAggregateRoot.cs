namespace EasyAccess.Infrastructure.Entity
{
    public interface IAggregateRoot : IEntity
    {
        IAggregateRoot this[object id] { get; }
    }
}
