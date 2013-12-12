using System.Collections.Generic;

namespace Demo.Model.DTOs
{
    public class AritcleSectionInputDataDto
    {
        public List<ArticleConfigDto> Articles { get; set; }

        public List<SectionConfigDto> Sections { get; set; }

        public List<InputConfigDto> Inputs { get; set; }

        public List<DataCollectionDto> Datas { get; set; }
    }
}
