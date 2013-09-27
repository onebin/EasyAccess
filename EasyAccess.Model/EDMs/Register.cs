using System;
using System.ComponentModel.DataAnnotations;

namespace EasyAccess.Model.EDMs
{
    public class Register
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string LoginName { get; set; }

        [Required]
        public string Password { get; set; }

        public Guid Salt { get; set; }

        [MaxLength(32)]
        public string LastLoginIP { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? LastLoginTime { get; set; }

        public Account Account { get; set; }
    }
}
