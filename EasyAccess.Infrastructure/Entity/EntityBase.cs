using System;
using System.ComponentModel.DataAnnotations;

namespace EasyAccess.Infrastructure.Entity
{
    public abstract class EntityBase<TKey> : IEntity<TKey> where TKey : struct
    {
        [Key]
        public TKey Id { get; set; }

        protected EntityBase()
        {
            IsDeleted = false;
            CreateTime = DateTime.Now;
        }

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
