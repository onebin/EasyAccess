namespace EasyAccess.Infrastructure.Entity
{
    public interface IEntity<TKey> where TKey : struct 
    {
        TKey Id { get; set; }
    }
}
