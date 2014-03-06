using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using EasyAccess.Infrastructure.Attr;
using EasyAccess.Infrastructure.Extensions;

namespace EasyAccess.Infrastructure.Util.CustomTimestamp
{
    internal class CustomTimestampCacheManager
    {
        public static  CustomTimestampCacheManager Instance { get; private set; }

        private readonly ReaderWriterLockSlim _rwLocker = new ReaderWriterLockSlim();

        private static readonly Dictionary<string, CustomTimestampCache> CustomTimestampCacheItems = new Dictionary<string, CustomTimestampCache>();

        static CustomTimestampCacheManager()
        {
            Instance = new CustomTimestampCacheManager();
        }

        public ICustomTimestampCache GetCacheItem(Type baseType)
        {
            _rwLocker.EnterUpgradeableReadLock();
            try
            {
                if (!CustomTimestampCacheItems.ContainsKey(baseType.FullName))
                {
                    Cache(baseType);
                }
            }
            finally
            {
                _rwLocker.ExitUpgradeableReadLock();
            }
            return CustomTimestampCacheItems[baseType.FullName];
        }

        private void Cache(Type baseType)
        {
            try
            {
                _rwLocker.EnterWriteLock();
                CustomTimestampCacheItems.Add(baseType.FullName, new CustomTimestampCache(baseType));
            }
            finally
            {

                _rwLocker.ExitWriteLock();
            }
        }

        class CustomTimestampCache : ICustomTimestampCache
        {
            private readonly Dictionary<string, string> _columnNames = new Dictionary<string, string>();

            public CustomTimestampCache(Type baseType)
            {
                TableName = baseType.Name;
                Properies = baseType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                BasicProperies = Properies.Where(x => x.PropertyType.IsBasic()).ToArray();

                var tableAttr = baseType.GetCustomAttributes<TableAttribute>(false).ToArray();
                if (tableAttr.Any())
                {
                    TableName = tableAttr[0].Name;
                }
                var customTimestampType = typeof(CustomTimestampAttribute);
                var customTimestampProperty = BasicProperies.FirstOrDefault(
                    basicPropery => basicPropery.CustomAttributes.Any(
                        x => x.AttributeType == customTimestampType));

                if (customTimestampProperty != null)
                {
                    HasTimestampProperty = true;
                    TimestampPropertyName = customTimestampProperty.Name;
                    TimestampColumnName = GetColumnName(customTimestampProperty);
                    UpdateMode = customTimestampProperty.GetCustomAttribute<CustomTimestampAttribute>().UpdateMode;
                }
            }

            public bool HasTimestampProperty { get; private set; }

            public string TableName { get; private set; }

            public string TimestampColumnName { get; private set; }

            public string TimestampPropertyName { get; private set; }

            public CustomTimestampUpdateMode UpdateMode { get; private set; }

            public PropertyInfo[] BasicProperies { get; private set; }
            public PropertyInfo[] Properies { get; private set; }

            public string GetColumnName(PropertyInfo property)
            {
                if (!_columnNames.ContainsKey(property.Name))
                {
                    var columnAttrs = property.GetCustomAttributes<ColumnAttribute>().ToArray();
                    _columnNames.Add(property.Name, columnAttrs.Any() ? columnAttrs[0].Name : property.Name);
                }
                return _columnNames[property.Name];
            }

            public string GetColumnValue(PropertyInfo property, DbEntityEntry entry)
            {
                var value = entry.Property(property.Name).CurrentValue.ToString();
                if (property.PropertyType.IsEnum)
                {
                    value = ((int)entry.Property(property.Name).CurrentValue).ToString(CultureInfo.InvariantCulture);
                }
                return value;
            }
        }
    }


}