using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasyAccess.Infrastructure.Entity;

namespace Demo.Model.EDMs
{
    public class SectionConfig : IAggregateRoot
    {
        object IEntity.Id
        {
            get
            {
                return this.Id;
            }
            set
            {
                this.Id = (int)value;
            }
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public virtual ArticleConfig Article { get; set; }

        public virtual InputConfig Input { get; set; }

        public virtual SectionConfig ParentSection { get; set; }

        public virtual ICollection<SectionConfig> SubSections { get; set; }

        public int? ParentId { get; set; }

        /// <summary>
        /// Section名称
        /// </summary>
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// 深度
        /// </summary>
        public int Depth { get; set; }

        /// <summary>
        /// 排序索引
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 可否重复添加
        /// </summary>
        public bool IsRepeatable { get; set; }
    }
}
