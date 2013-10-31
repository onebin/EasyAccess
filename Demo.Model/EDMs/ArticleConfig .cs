using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EasyAccess.Infrastructure.Entity;

namespace Demo.Model.EDMs
{
    public class ArticleConfig : AggregateRootBase<int>
    {
        public virtual List<SectionConfig> Sections { get; set; }

        /// <summary>
        /// Article名称
        /// </summary>
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// 排序索引
        /// </summary>
        public int Index { get; set; }
    }
}
