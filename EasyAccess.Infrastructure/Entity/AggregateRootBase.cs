using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyAccess.Infrastructure.Entity
{
    public abstract class AggregateRootBase<TKey> : AggregateBase<TKey>, IAggregateRootBase<TKey>
    {
    }
}
