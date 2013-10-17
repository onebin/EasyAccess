using System.Collections.Generic;
using EasyAccess.Infrastructure.Entity;
using EasyAccess.Model.DTOs;

namespace EasyAccess.Model.EDMs
{
    public class Account : AggregateRootBase<long>
    {
        public int Age { get; set; }

        public Sex Sex { get; set; }

        public string Memo { get; set; }

        public bool IsDeleted { get; set; }

        public Name Name { get; set; }

        public Contact Contact { get; set; }

        public virtual Register Register { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
