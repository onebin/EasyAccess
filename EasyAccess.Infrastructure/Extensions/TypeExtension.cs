using System;

namespace EasyAccess.Infrastructure.Extensions
{
    public static class TypeExtension
    {
        /// <summary>
        /// 是否Nullable&lt;T&gt;类型
        /// </summary>
        public static bool IsNullableType(this Type type)
        {
            return type != null && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// 是否DateTime的Nullable类型
        /// </summary>
        public static bool IsNullableDateTime(this Type type)
        {
            return type.IsNullableType() && Nullable.GetUnderlyingType(type) == typeof (DateTime);
        }

        /// <summary>
        /// 如果类型是Nullable&lt;T&gt;，则返回T，否则返回自身
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetNonNullableType(this Type type)
        {
            if (IsNullableType(type))
            {
                return type.GetGenericArguments()[0];
            }
            return type;
        }
    }
}
