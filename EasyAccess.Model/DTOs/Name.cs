using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAccess.Model.DTOs
{
    [ComplexType]
    public class Name
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string NickName { get; set; }

        public override string ToString()
        {
            return LastName + FirstName;
        }
    }
}
