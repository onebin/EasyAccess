using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Demo.Model.VOs;
using EasyAccess.Infrastructure.Entity;

namespace Demo.Model.EDMs
{
    public class InputConfig : AggregateRootBase<InputConfig, int>
    {
        public virtual SectionConfig Section { get; set; }

        /// <summary>
        /// 输入类型
        /// </summary>
        [Required]
        public InputType InputType { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        [Required]
        public bool IsRequired { get; set; }

        /// <summary>
        /// 验证类型
        /// </summary>
        [MaxLength(512)]
        public string ValidType { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        [MaxLength(255)]
        public string DefaultValue { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 提示
        /// </summary>
        [MaxLength(255)]
        public string Tips { get; set; }
    }
}
