using System.Collections.Generic;
using EasyAccess.Infrastructure.Entity;

namespace EasyAccess.Model.EDMs
{
    public class Menu : AggregateBase<string>
    {
        public string ParentId { get; set; }

        public string Name { get; set; }

        public string System { get; set; }

        public string Url { get; set; }

        public int Depth { get; set; }

        public int Index { get; set; }

        public virtual Menu ParentMenu { get; set; }

        public virtual ICollection<Menu> SubMenus { get; set; } 

        public virtual ICollection<Permission> Permissions { get; set; } 
    }
}
