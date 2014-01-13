using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using EasyAccess.Infrastructure.Extensions;
using Spring.Collections;
using Spring.Expressions;

namespace EasyAccess.Infrastructure.Util.DataConverter
{
    public static class ConvertToList
    {
        public static List<T> ToList<T>(this DataConverter<T> dataConverter, DataTable dataTable, ConvertToListOptions<T> options = null) where T : class, new()
        {
            InitOptions(dataConverter, ref options);
            ValidateColumnMapper<T>(dataTable, options);
            return Conver(dataConverter, dataTable, options);
        }

        private static List<T> Conver<T>(DataConverter<T> dataConverter, DataTable dataTable, ConvertToListOptions<T> options) where T : class, new()
        {
            var lst = new List<T>();
            foreach (DataRow row in dataTable.Rows)
            {
                var item = new T();
                foreach (var columnInfo in options.ColumnMapper)
                {
                    if (columnInfo.Value.PropertyType.IsEnum)
                    {
                        columnInfo.Value.SetValue(item, Enum.Parse(columnInfo.Value.PropertyType, row[columnInfo.Key].ToString()));
                    }
                    else
                    {
                        columnInfo.Value.SetValue(item, Convert.ChangeType(row[columnInfo.Key], columnInfo.Value.PropertyType));
                    }
                }
                lst.Add(item);
            }
            return lst;
        }

        private static void InitOptions<T>(DataConverter<T> dataConverter, ref ConvertToListOptions<T> options) where T : class
        {
            if (options == null)
            {
                options = new ConvertToListOptions<T>();
                foreach (var property in dataConverter.PropertyToShow)
                {
                    options.MapColumn(property.Name, property);
                }
            }
        }

        private static void ValidateColumnMapper<T>(DataTable dataTable, ConvertToListOptions<T> options) where T : class
        {
            var requireColumns = options.ColumnMapper.Select(x => x.Key);
            var missColumns = requireColumns.Where(x => !dataTable.Columns.Contains(x)).ToArray();
            if (missColumns.Count() != 0)
            {
                throw new KeyNotFoundException("缺少列名为：【" + string.Join("】,【", missColumns) + "】的配置信息");
            }
        }
    }

    public class ConvertToListOptions<T> where T: class 
    {
        public Dictionary<string, PropertyInfo> ColumnMapper { get; private set; } 

        public ConvertToListOptions()
        {
            ColumnMapper = new Dictionary<string,PropertyInfo>();
        }

        public ConvertToListOptions<T> MapColumn(Expression<Func<T, object>> expr, string columnName)
        {
            PropertyInfo propertyInfo; 
            if (expr.Body is UnaryExpression)
            {
                propertyInfo = ((MemberExpression)((UnaryExpression)expr.Body).Operand).Member as PropertyInfo;
            }
            else if (expr.Body is MemberExpression)
            {
                propertyInfo = ((MemberExpression)expr.Body).Member as PropertyInfo;
            }
            else
            {
                throw new NotSupportedException();
            }
            return MapColumn(columnName, propertyInfo);
        }

        public ConvertToListOptions<T> MapColumn(string columnName, PropertyInfo propertyInfo)
        {
            if (!ColumnMapper.ContainsKey(columnName))
            {
                ColumnMapper.Add(columnName, propertyInfo);
                return this;
            }
            else
            {
                throw new ArgumentException("列名【" + columnName + "】重复配置");
            }
        }
    }
}