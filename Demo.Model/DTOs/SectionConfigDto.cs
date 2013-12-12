namespace Demo.Model.DTOs
{
    public class SectionConfigDto
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public int ArticleId { get; set; }

        public int? InputId { get; set; }

        public string Name { get; set; }

        public int Index { get; set; }

        public int Depth { get; set; }

        public bool IsRepeatable { get; set; }

        public string TreeFlag { get; set; }

    }
}