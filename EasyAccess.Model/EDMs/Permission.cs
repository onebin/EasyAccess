using System.Collections.Generic;
using EasyAccess.Infrastructure.Entity;

namespace EasyAccess.Model.EDMs
{
    public class Permission : EntityBase<string>
    {
        public string MenuId { get; set; }

        public Menu Menu { get; set; }

        public string Name { get; set; }

        public string ActionUrl { get; set; }

        public virtual ICollection<Role> Roles { get; set; } 
    }
}
