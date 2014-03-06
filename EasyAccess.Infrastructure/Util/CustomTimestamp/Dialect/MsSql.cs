using System.Data.Entity.Infrastructure;
using System.Linq;

namespace EasyAccess.Infrastructure.Util.CustomTimestamp.Dialect
{
    internal class MsSql : SqlCommandBase
    {
        public MsSql(DbEntityEntry entry) : base(entry) { }

        public override string Update()
        {
            var columnNameAndValues = GetUpdateColumnNameAndValues();
            if (columnNameAndValues.Any())
            {
                var sets = columnNameAndValues.Select(x => "[" + x.Key + "] = " + "'" + x.Value + "'");
                return "UPDATE [" + CustomTimestampCache.TableName + "] SET " + string.Join(", ", sets)
                       + " WHERE [Id] = '" + DbEntityEntry.Property("Id").CurrentValue + "' " + GetTimestampCondition() + ";";
            }
            return string.Empty;
        }

        private string GetTimestampCondition()
        {
            var condition = "AND [" + CustomTimestampCache.TimestampColumnName + "]";

            switch (CustomTimestampCache.UpdateMode)
            {
                case CustomTimestampUpdateMode.GreaterThan:
                    condition += " < '" + DbEntityEntry.Property(CustomTimestampCache.TimestampPropertyName).CurrentValue + "'";
                    break;
                default:
                    condition += " = '" + DbEntityEntry.Property(CustomTimestampCache.TimestampPropertyName).OriginalValue +"'";
                    break;
            }
            return condition;
        }
    }
}
