using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EasyAccess.Model.DTOs;

namespace EasyAccess.Model.EDMs
{
    public class Account
    {
        public long Id { get; set; }

        public int Sex { get; set; }

        public string Memo { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsDeleted { get; set; }

        public Name Name { get; set; }

        public Contact Contact { get; set; }

        public virtual Register Register { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
