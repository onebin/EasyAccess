using System;
using System.Collections;
using System.Reflection;

namespace EasyAccess.Infrastructure.Util.EnumDescription
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum)]
    public class EnumDescriptionAttribute : Attribute, IEnumDescription
    {
        /// <summary>
        /// 描述枚举值
        /// </summary>
        /// <param name="description">描述内容</param>
        /// <param name="index">排列顺序</param>
        public EnumDescriptionAttribute(string description, int index)
        {
            Description = description;
            Index = index;
        }

        /// <summary>
        /// 描述枚举值，默认排序为10
        /// </summary>
        /// <param name="description">描述内容</param>
        public EnumDescriptionAttribute(string description) : this(description, 10) { }


        public string Description { get; private set; }

        public int Index { get; private set; }

        public int Value { get; set; }

        public string Name { get; set; }

    }
}
