using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyAccess.Infrastructure.Entity
{
    public interface IAggregateBase<TKey> : IEntity
    {
        new TKey Id { get; set; }
    }
}
