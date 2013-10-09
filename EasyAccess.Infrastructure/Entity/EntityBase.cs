using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyAccess.Infrastructure.Entity
{
    public abstract class EntityBase<TKey> where TKey : struct 
    {
        protected EntityBase()
        {
            IsDeleted = false;
            CreateTime = DateTime.Now;
        }

        [Key]
        public TKey Id { get; set; }

        /// <summary>
        /// 获取或设置 获取或设置是否禁用，逻辑上的删除，非物理删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 获取或设置 添加时间
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime CreateTime { get; set; }
    }
}
