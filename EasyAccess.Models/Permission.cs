using System;
using System.Collections.Generic;

namespace EasyAccess.Models
{
    public class Permission
    {
        public string Id { get; set; }

        public string MenuId { get; set; }

        public Menu Menu { get; set; }

        public string Name { get; set; }

        public string ActionUrl { get; set; }

        public string ActionName { get; set; }

        public virtual ICollection<Role> Roles { get; set; } 
    }
}
