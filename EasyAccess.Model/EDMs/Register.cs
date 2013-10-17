using System;
using System.ComponentModel.DataAnnotations;
using EasyAccess.Infrastructure.Entity;
using EasyAccess.Model.DTOs;

namespace EasyAccess.Model.EDMs
{
    public class Register : AggregateBase<long>
    {
        public LoginUser LoginUser { get; set; }

        public Guid Salt { get; set; }

        [MaxLength(32)]
        public string LastLoginIP { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? LastLoginTime { get; set; }

        public virtual Account Account { get; set; }
    }
}
