using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace EasyAccess.Infrastructure.Util.CustomTimestamp
{
    internal abstract class SqlCommandBase : ISqlCommand
    {
        protected SqlCommandBase(DbEntityEntry entry)
        {
            DbEntityEntry = entry;
            CustomTimestampCache = CustomTimestampCacheManager.Instance.GetCacheItem(entry.Entity.GetType());
        }

        protected DbEntityEntry DbEntityEntry { get; set; }

        protected ICustomTimestampCache CustomTimestampCache { get; set; }

        protected Dictionary<string, CustomDbParameter> GetUpdateColumnNameAndValues()
        {
            var columnNameAndValues = new Dictionary<string, CustomDbParameter>();
            var columnNameCount = new Dictionary<string, int>();
            if (CustomTimestampCache.HasTimestampProperty && DbEntityEntry.State == EntityState.Modified)
            {
                foreach (var basicProperty in CustomTimestampCache.BasicProperies)
                {
                    if (DbEntityEntry.Property(basicProperty.Name).IsModified)
                    {
                        var columnName = CustomTimestampCache.GetColumnName(basicProperty);
                        if (columnNameCount.ContainsKey(columnName))
                        {
                            ++columnNameCount[columnName];
                        }
                        else
                        {
                            columnNameCount.Add(columnName, 0);
                        }
                        columnNameAndValues.Add(columnName, new CustomDbParameter("@p" + columnName + "_" + columnNameCount[columnName], CustomTimestampCache.GetColumnValue(basicProperty, DbEntityEntry)));
                    }
                }
            }
            return columnNameAndValues;
        }

        public abstract DbCommand Update();
    }
}