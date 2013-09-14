using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace EasyAccess.Models
{
    public class Account
    {
        public long Id { get; set; }
        
        [MaxLength(50)]
        public string LoginName { get; set; }

        public string Password { get; set; }

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

        [MaxLength(32)]
        public string LastLoginIP { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? LastLoginTime { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
