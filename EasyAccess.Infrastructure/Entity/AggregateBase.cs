using System.ComponentModel.DataAnnotations;

namespace EasyAccess.Infrastructure.Entity
{
    public abstract class AggregateBase<TKey> : IAggregateBase<TKey>
    {
        [Key]
        public virtual TKey Id { get; set; }


        object IEntity.Id
        {
            get { return Id; }
            set { this.Id = (TKey)value; }
        }
    }
}
