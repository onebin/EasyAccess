using System;
using System.ComponentModel.DataAnnotations;

namespace EasyAccess.Infrastructure.Entity
{
    public abstract class EntityBase<TKey> : IEntity<TKey>
    {
        [Key]
        public TKey Id { get; set; }

    }
}
