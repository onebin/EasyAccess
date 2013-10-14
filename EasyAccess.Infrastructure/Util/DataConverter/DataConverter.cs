using System;
using System.Collections.Generic;
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

        public DataConverter<T> AddFieldFormatter(string filedName, string newFieldName)
        {
            this.FieldFormatter.Add(filedName, newFieldName);
            return this;
        }

        protected int CheckPropertyToShow()
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                if (property.PropertyType.IsBaseDataType())
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
            if (property.PropertyType.IsNullableOfDateTime())
            {
                var dateTime = property.GetValue(data, null) as DateTime?;
                val = dateTime != null ? dateTime.Value.ToString(@"yyyy\/MM\/dd HH:mm:ss") : "";
            }
            else
            {
                var propertyVal = property.GetValue(data, null);
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
