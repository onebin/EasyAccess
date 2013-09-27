using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyAccess.Model.EDMs
{
    public class Account
    {
        public long Id { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        public int Sex { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }

        [MaxLength(32)]
        public string Phone { get; set; }

        public string Memo { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsDeleted { get; set; }

        public Register Register { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
