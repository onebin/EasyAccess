using System;
using System.ComponentModel.DataAnnotations;

namespace EasyAccess.Infrastructure.Entity
{
    public abstract class AggregateBase<TKey> : IAggregateBase<TKey>
    {
        [Key]
        public TKey Id { get; set; }


        object IEntity.Id
        {
            get { return Id; }
            set { this.Id = (TKey)value; }
        }
    }
}
