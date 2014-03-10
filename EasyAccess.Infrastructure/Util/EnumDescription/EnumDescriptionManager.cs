using System;
using System.Collections;
using System.Reflection;

namespace EasyAccess.Infrastructure.Util.EnumDescription
{
    public class EnumDescriptionManager
    {
        public static EnumDescriptionManager Instance { get; private set; }

        private static readonly Hashtable CachedEnum = new Hashtable();


        static EnumDescriptionManager()
        {
            Instance = new EnumDescriptionManager();
        }

        /// <summary>
        /// 获得指定枚举类型中，指定值的描述文本。
        /// </summary>
        /// <param name="name">枚举值，不要作任何类型转换</param>
        /// <returns>描述字符串</returns>
        public string GetDescription(object name)
        {
            var descriptions = GetEnumDescriptions(name.GetType());
            foreach (var enumDescription in descriptions)
            {
                if (enumDescription.Name == name.ToString()) return enumDescription.Description;
            }
            return string.Empty;
        }

        /// <summary>
        /// 得到枚举类型定义的所有文本，按定义的顺序返回
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
        /// <param name="enumType">枚举类型</param>
        /// <param name="sortType">指定排序类型</param>
        /// <returns>所有定义的文本</returns>
        public IEnumDescription[] GetEnumDescriptions(Type enumType, EnumSortType sortType = EnumSortType.Default)
        {
            IEnumDescription[] descriptions = null;
            //缓存中没有找到，通过反射获得字段的描述信息
            if (!CachedEnum.Contains(enumType.FullName))
            {
                var fields = enumType.GetFields();
                var edAl = new ArrayList();
                foreach (var field in fields)
                {
                    var eds = field.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
                    if (eds.Length != 1) continue;
                    ((EnumDescriptionAttribute)eds[0]).Name = field.Name;
                    ((EnumDescriptionAttribute)eds[0]).Value = (int)field.GetValue(enumType);
                    edAl.Add(eds[0]);
                }

                CachedEnum.Add(enumType.FullName, edAl.ToArray(typeof(IEnumDescription)));
            }
            descriptions = (IEnumDescription[])CachedEnum[enumType.FullName];

            if (descriptions.Length <= 0) throw new NotSupportedException("枚举类型[" + enumType.Name + "]未定义属性EnumValueDescription");


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
                                if (String.CompareOrdinal(descriptions[m].Description, descriptions[n].Description) > 0) swap = true;
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