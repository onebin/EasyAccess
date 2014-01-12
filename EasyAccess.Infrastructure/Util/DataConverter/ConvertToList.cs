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

        private static ConvertToListOptions _options;

        public static List<T> ToList<T>(this DataConverter<T> dataConverter, DataTable dataTable, ConvertToListOptions options = null) where T : class, new()
        {
            InitOptions(dataConverter, options);
            Validate<T>(dataTable);
            return Conver(dataConverter, dataTable);
        }

        private static List<T> Conver<T>(DataConverter<T> dataConverter, DataTable dataTable) where T : class, new()
        {
            var lst = new List<T>();
            var typeName = typeof (T).Name;
            foreach (DataRow row in dataTable.Rows)
            {
                var item = new T();
                foreach (var columnInfo in _options.ColumnMapper[typeName])
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

        private static void InitOptions<T>(DataConverter<T> dataConverter, ConvertToListOptions options) where T : class
        {
            if (options == null)
            {
                var typeName = typeof(T).Name;
                _options = new ConvertToListOptions();
                _options.MapTable(typeName, typeName);
                foreach (var property in dataConverter.PropertyToShow)
                {
                    _options.MapColumn(typeName, property.Name, property);
                }
            }
            else
            {
                _options = options;
            }
        }

        private static void Validate<T>(DataTable dataTable)
        {
            ValidateTable(dataTable);
            ValidateColumn<T>(dataTable);
        }

        private static void ValidateTable(DataTable dataTable)
        {
            if (!_options.TableMapper.ContainsKey(dataTable.TableName))
            {
                throw new KeyNotFoundException("未找到表名为【" + dataTable.TableName + "】的配置信息");
            };
        }

        private static void ValidateColumn<T>(DataTable dataTable)
        {
            var typeName = typeof(T).Name;
            var requireColumns = _options.ColumnMapper[typeName].Select(x => x.Key);
            var missColumns = requireColumns.Where(x => !dataTable.Columns.Contains(x)).ToArray();
            if (missColumns.Count() != 0)
            {
                throw new KeyNotFoundException("缺少列名为：【" + string.Join("】,【", missColumns) + "】的配置信息");
            }
        }
    }

    public class ConvertToListOptions
    {
        public ConvertToListOptions()
        {
            TableMapper = new Dictionary<string, string>();
            ColumnMapper = new Dictionary<string, Dictionary<string, PropertyInfo>>();
        }

        public Dictionary<string, string> TableMapper { get; private set; }
        public Dictionary<string, Dictionary<string, PropertyInfo>> ColumnMapper { get; private set; } 


        public void MapTable(Type type, string tableName)
        {
            MapTable(type.Name, tableName);
        }

        public void MapTable(string typeName, string tableName)
        {
            if (!TableMapper.ContainsKey(tableName))
            {
                TableMapper.Add(tableName, typeName);
                ColumnMapper.Add(typeName, new Dictionary<string, PropertyInfo>());
            }
            else
            {
                throw new ArgumentException("表名【" + tableName + "】重复配置");
            }
        }

        public void MapColumn<T>(Expression<Func<T, object>> expr, string columnName)
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
            MapColumn(typeof(T).Name, columnName, propertyInfo);
        }

        public void MapColumn(string typeName, string columnName, PropertyInfo propertyInfo)
        {
            if (ColumnMapper.ContainsKey(typeName))
            {
                if (!ColumnMapper[typeName].ContainsKey(columnName))
                {
                    ColumnMapper[typeName].Add(columnName, propertyInfo);
                }
                else
                {
                    throw new ArgumentException("列名【" + columnName + "】重复配置");
                }
            }
            else
            {
                throw new KeyNotFoundException("未找到【" + typeName + "】类型的配置信息");
            }
        }
    }
}