using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyAccess.Model.Complex;

namespace EasyAccess.Model.DTOs
{
    public class AccountDto
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public Sex Sex { get; set; }

        public string Memo { get; set; }

        public bool IsDeleted { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
