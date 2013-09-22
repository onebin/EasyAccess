﻿using System.Collections.Generic;

namespace EasyAccess.Model.EDMs
{
    public class Menu
    {
        public string Id { get; set; }

        public string ParentId { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string System { get; set; }

        public int Leval { get; set; }

        public int Index { get; set; }

        public virtual Menu ParentMenu { get; set; }

        public virtual ICollection<Menu> SubMenus { get; set; } 

        public virtual ICollection<Permission> Permissions { get; set; } 
    }
}
