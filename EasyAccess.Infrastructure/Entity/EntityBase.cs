using System;
using System.ComponentModel.DataAnnotations;

namespace EasyAccess.Infrastructure.Entity
{
    public abstract class EntityBase<TKey> : IEntityBase<TKey>
    {
        [Key]
        public TKey Id { get; set; }

    }
}
