using System.Collections.Generic;
using EasyAccess.Infrastructure.Entity;

namespace EasyAccess.Model.EDMs
{
    public class Role : EntityBase<long>, IAggregateRoot<long>
    {
        public string Name { get; set; }

        public string HomePage { get; set; }

        public string Memo { get; set; }

        public bool IsEnabled { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; } 
    }
}
