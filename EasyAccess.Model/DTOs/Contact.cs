using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAccess.Model.DTOs
{
    [ComplexType]
    public class Contact
    {
        [MaxLength(255)]
        public string Email { get; set; }

        [MaxLength(32)]
        public string Phone { get; set; }
    }
}
