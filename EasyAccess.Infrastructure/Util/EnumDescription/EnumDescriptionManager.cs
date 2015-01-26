using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace EasyAccess.Infrastructure.Util.EnumDescription
{
    public class EnumDescriptionManager
    {
        private static readonly Hashtable CachedEnum = new Hashtable();
        private static object _locker = new object();


        static EnumDescriptionManager() {}

        /// <summary>
        /// 获得指定枚举类型中，指定值的描述文本。
        /// </summary>
        /// <param name="enumObj">枚举值，不要作任何类型转换</param>
        /// <returns>描述字符串</returns>
        public static string GetDescription<T>(T enumObj) where T : struct
        {
            var descriptions = GetEnumDescriptions(typeof(T));
            var description = descriptions.FirstOrDefault(x => x.Name == enumObj.ToString());
            return description == null ? string.Empty : description.Description;
        }

        public static string GetDescription<T>(string enumName, bool ignoreCase = false) where T : struct
        {
            var description = GetEnumDescription<T>(enumName, ignoreCase);
            return description == null ? string.Empty : description.Description;
        }

        public static IEnumDescription GetEnumDescription<T>(int enumValue) where T : struct
        {
            var descriptions = GetEnumDescriptions(typeof(T));
            return descriptions.FirstOrDefault(x => x.Value == enumValue);
        }

        public static IEnumDescription GetEnumDescription<T, TValue>(TValue enumValue) 
            where T : struct
            where TValue : struct
        {
            var enumVal = 0;
            if (!int.TryParse(enumValue.ToString(), out enumVal))
            {
                throw new ArgumentException("无法转换类型:" + typeof(TValue) + ":" + enumVal);
            }
            return GetEnumDescription<T>(enumVal);
        }

        public static IEnumDescription GetEnumDescription<T>(string enumName, bool ignoreCase = false) where T : struct
        {
            var descriptions = GetEnumDescriptions(typeof(T));
            IEnumDescription description;
            enumName = enumName.Trim();
            if (ignoreCase)
            {
                enumName = enumName.ToLower();
                description = descriptions.FirstOrDefault(x => x.Name.ToLower().Trim() == enumName);
            }
            else
            {
                description = descriptions.FirstOrDefault(x => x.Name.Trim() == enumName);
            }
            return description;
        }

        /// <summary>
        /// 得到枚举类型定义的所有文本，按定义的顺序返回
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
        /// <param name="enumType">枚举类型</param>
        /// <param name="sortType">指定排序类型</param>
        /// <returns>所有定义的文本</returns>
        public static IEnumDescription[] GetEnumDescriptions(Type enumType, EnumSortType sortType = EnumSortType.Default)
        {
            if (!CachedEnum.Contains(enumType.FullName))
            {
                lock (_locker)
                {
                    if (!CachedEnum.Contains(enumType.FullName))
                    {
                        var fields = enumType.GetFields();
                        var descArrayList = new ArrayList();
                        var enumIdx = 0;
                        foreach (var field in fields)
                        {
                            if (field.IsSpecialName)
                            {
                                continue;
                            }
                            var enumDescAttrs = field.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
                            EnumDescriptionAttribute enumDescAttr;
                            if (enumDescAttrs.Any())
                            {
                                enumDescAttr = ((EnumDescriptionAttribute)enumDescAttrs[0]);
                            }
                            else
                            {
                                var descAttrs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                                var descStr = field.Name;
                                if (descAttrs.Any())
                                {
                                    descStr = ((DescriptionAttribute)descAttrs[0]).Description;
                                }
                                enumDescAttr = new EnumDescriptionAttribute(descStr, enumIdx++);
                            }
                            enumDescAttr.Name = field.Name;
                            enumDescAttr.Value = field.GetValue(enumType).GetHashCode();
                            descArrayList.Add(enumDescAttr);
                        }
                        ;
                        CachedEnum.Add(enumType.FullName, descArrayList.ToArray(typeof(IEnumDescription)));
                    }
                }
            }
            var descriptions = (IEnumDescription[])CachedEnum[enumType.FullName];

            //默认就不排序了
            if (sortType != EnumSortType.Default)
            {
                //按指定的属性冒泡排序
                for (int m = 0; m < descriptions.Length; m++)
                {
                    for (int n = m; n < descriptions.Length; n++)
                    {
                        var swap = false;

                        switch (sortType)
                        {
                            case EnumSortType.Description:
                                if (string.CompareOrdinal(descriptions[m].Description, descriptions[n].Description) > 0) swap = true;
                                break;
                            case EnumSortType.Index:
                                if (descriptions[m].Index > descriptions[n].Index) swap = true;
                                break;
                        }

                        if (swap)
                        {
                            var temp = descriptions[m];
                            descriptions[m] = descriptions[n];
                            descriptions[n] = temp;
                        }
                    }
                }

            };

            return descriptions;
        }
    }
}