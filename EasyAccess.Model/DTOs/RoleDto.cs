namespace EasyAccess.Model.DTOs
{
    public class RoleDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string HomePage { get; set; }

        public string Memo { get; set; }

        public bool IsEnabled { get; set; } 
    }
}