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
        /// 如果类型是Nullable&lt;T&gt;，则返回T，否则返回自身
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetNonNullableType(this Type type)
        {
            if (IsNullableType(type))
            {
                return Nullable.GetUnderlyingType(type);
            }
            return type;
        }

        /// <summary>
        /// 是否为可空的target类型
        /// </summary>
        public static bool IsNullableOf(this Type type, Type target)
        {
            return type.IsNullableType() && Nullable.GetUnderlyingType(type) == target;
        }

        /// <summary>
        /// 是否基本数据类型
        /// </summary>
        public static bool IsBasic(this Type type)
        {
            return type.IsValueType || type == typeof(string);
        }

        /// <summary>
        /// 是否为数值类型
        /// </summary>
        public static bool IsNumeric(this Type type)
        {
            return type == typeof(Byte)
                || type == typeof(Int16)
                || type == typeof(Int32)
                || type == typeof(Int64)
                || type == typeof(SByte)
                || type == typeof(UInt16)
                || type == typeof(UInt32)
                || type == typeof(UInt64)
                || type == typeof(Decimal)
                || type == typeof(Double)
                || type == typeof(Single);
        }
    }
}
