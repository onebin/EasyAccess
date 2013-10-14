using System.Collections.Generic;
using EasyAccess.Infrastructure.Entity;
using EasyAccess.Model.DTOs;

namespace EasyAccess.Model.EDMs
{
    public class Account : EntityBase<long>, IAggregateRootBase<long>
    {
        public int Sex { get; set; }

        public string Memo { get; set; }

        public bool IsDeleted { get; set; }

        public Name Name { get; set; }

        public Contact Contact { get; set; }

        public virtual Register Register { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
