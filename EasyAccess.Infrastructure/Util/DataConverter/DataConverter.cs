using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using EasyAccess.Infrastructure.Extensions;

namespace EasyAccess.Infrastructure.Util.DataConverter
{
    public class DataConverter<T> where T : class
    {
        public DataConverter()
        {
            CheckPropertyToShow();
        }

        public Dictionary<string, Dictionary<string, string>> DataFormatter = new Dictionary<string, Dictionary<string, string>>();

        public Dictionary<string, string> FieldFormatter = new Dictionary<string, string>();

        public List<PropertyInfo> PropertyToShow = new List<PropertyInfo>();

        public DataConverter<T> AddDataFormatter(string filedName, Dictionary<string, string> dic)
        {
            this.DataFormatter.Add(filedName, dic);
            return this;
        }

        public DataConverter<T> AddDataFormatterr<TKey>(Expression<Func<T, TKey>> expr, Dictionary<string, string> dic)
        {
            this.DataFormatter.Add(GetPropertyName<TKey>(expr), dic);
            return this;
        }

        public DataConverter<T> AddFieldFormatter(string filedName, string newFieldName)
        {
            this.FieldFormatter.Add(filedName, newFieldName);
            return this;
        }

        public DataConverter<T> AddFieldFormatter<TKey>(Expression<Func<T, TKey>> expr, string newFieldName)
        {
            this.AddFieldFormatter(GetPropertyName<TKey>(expr), newFieldName);
            return this;
        }

        protected string GetPropertyName<TKey>(Expression<Func<T, TKey>> expr)
        {
            if (expr.Body is MemberExpression)
            {
                return ((MemberExpression) expr.Body).Member.Name;
            }
            throw new ArgumentException("请传递泛型参数属性");
        }

        protected int CheckPropertyToShow()
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                if (property.PropertyType.IsBasic())
                {
                    PropertyToShow.Add(property);
                }
            }
            return PropertyToShow.Count;
        }

        public void GetKeyValFromDataProperty(T data, PropertyInfo property, out string key, out string val)
        {
            if (!FieldFormatter.TryGetValue(property.Name, out key))
            {
                key = property.Name;
            }
            if (property.PropertyType.IsNullableOf(typeof(DateTime)))
            {
                var dateTime = property.GetValue(data, null) as DateTime?;
                val = dateTime != null ? dateTime.Value.ToString(@"yyyy\/MM\/dd HH:mm:ss") : "";
            }
            else
            {
                object propertyVal;
                var nonNullableType = property.PropertyType.GetNonNullableType();
                if (nonNullableType.IsEnum)
                {
                    propertyVal = (int)property.GetValue(data, null);
                }
                else
                {
                    propertyVal = property.GetValue(data, null);
                }
                Dictionary<string, string> dic;
                val = propertyVal == null ? string.Empty : propertyVal.ToString();
                if (DataFormatter.TryGetValue(property.Name, out dic))
                {
                    string dicVal;
                    if (dic.TryGetValue(val, out dicVal))
                    {
                        val = dicVal;
                    }
                }
            }
        }
    }
}
