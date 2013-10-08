using System;
using System.ComponentModel.DataAnnotations;
using EasyAccess.Model.DTOs;

namespace EasyAccess.Model.EDMs
{
    public class Register
    {
        public long Id { get; set; }

        public LoginUser LoginUser { get; set; }

        public Guid Salt { get; set; }

        [MaxLength(32)]
        public string LastLoginIP { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? LastLoginTime { get; set; }

        public virtual Account Account { get; set; }
    }
}
