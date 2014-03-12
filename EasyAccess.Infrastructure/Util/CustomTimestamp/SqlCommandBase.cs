using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

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

        protected Dictionary<string, string> GetUpdateColumnNameAndValues()
        {
            var columnNameAndValues = new Dictionary<string, string>();
            if (CustomTimestampCache.HasTimestampProperty && DbEntityEntry.State == EntityState.Modified)
            {
                foreach (var basicProperty in CustomTimestampCache.BasicProperies)
                {
                    if (DbEntityEntry.Property(basicProperty.Name).IsModified)
                    {
                        columnNameAndValues.Add(CustomTimestampCache.GetColumnName(basicProperty), CustomTimestampCache.GetColumnValue(basicProperty, DbEntityEntry));
                    }
                }
            }
            return columnNameAndValues;
        }

        public abstract string Update();
    }
}