using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Demo.Model.DTOs;
using Demo.Model.VOs;
using EasyAccess.Infrastructure.Entity;

namespace Demo.Model.EDMs
{
    public class InputConfig : AggregateBase<int>
    {
        #region 属性

        #region 引用关联

        public virtual SectionConfig Section { get; set; }
        
        #endregion

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

        #endregion

        #region 实例方法

        public void Update(InputConfigDto dto)
        {
            this.InputType = dto.InputType;
            this.IsRequired = dto.IsRequired;
            this.ValidType = dto.ValidType;
            this.DefaultValue = dto.DefaultValue;
            this.Tips = dto.Tips;
            this.Memo = dto.Memo;
        }
        
        #endregion
    }
}
