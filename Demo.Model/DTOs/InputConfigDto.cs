using Demo.Model.VOs;

namespace Demo.Model.DTOs
{
    public class InputConfigDto
    {
        public int Id { get; set; }

        public int SectionId { get; set; }

        public InputType InputType { get; set; }

        public bool IsRequired { get; set; }

        public string ValidType { get; set; }

        public string DefaultValue { get; set; }

        public string Memo { get; set; }

        public string Tips { get; set; }
    }
}
