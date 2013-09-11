using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace EasyAccess.Models
{
    public class Account
    {
        public long Id { get; set; }

        public string LoginName { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Sex { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Memo { get; set; }

        public string LastLoginIP { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? LastLoginTime { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
