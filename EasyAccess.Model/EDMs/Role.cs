using System.Collections.Generic;

namespace EasyAccess.Model.EDMs
{
    public class Role
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string HomePage { get; set; }

        public string Memo { get; set; }

        public bool Enabled { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; } 
    }
}
