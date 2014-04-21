using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using AutoMapper;

namespace EasyAccess.Infrastructure.Util.CustomTimestamp.Dialect
{
    internal class MsSql : SqlCommandBase
    {
        public MsSql(DbEntityEntry entry) : base(entry) { }

        public override DbCommand Update()
        {
            var command = new SqlCommand();
            var columnNameAndValues = GetUpdateColumnNameAndValues();
            if (columnNameAndValues.Any())
            {
                var sets = columnNameAndValues.Select(x => "[" + x.Key + "] = " + x.Value.ParameterName);
                command.CommandText = "UPDATE [" + CustomTimestampCache.TableName + "] SET " + string.Join(", ", sets)
                       + " WHERE [Id] = '" + DbEntityEntry.Property("Id").CurrentValue + "' AND " + GetTimestampCondition() + ";";

                command.Parameters.AddRange(columnNameAndValues.Select(x => new SqlParameter(x.Value.ParameterName, x.Value.Value)).ToArray());
            }
            return command;
        }

        private string GetTimestampCondition()
        {
            var condition = "[" + CustomTimestampCache.TimestampColumnName + "]";

            switch (CustomTimestampCache.UpdateMode)
            {
                case CustomTimestampUpdateMode.Equal:
                    condition += " = '" + DbEntityEntry.Property(CustomTimestampCache.TimestampPropertyName).OriginalValue + "'";
                    break;
                case CustomTimestampUpdateMode.GreaterThan:
                    condition += " < '" + DbEntityEntry.Property(CustomTimestampCache.TimestampPropertyName).CurrentValue + "'";
                    break;
                case CustomTimestampUpdateMode.GreaterThanOrEqual:
                    condition = "(" + condition + " < '" + DbEntityEntry.Property(CustomTimestampCache.TimestampPropertyName).CurrentValue + "' OR "
                        + condition + " = '" + DbEntityEntry.Property(CustomTimestampCache.TimestampPropertyName).OriginalValue + "'";
                    break;
                default:
                    condition = "1 = 1";
                    break;
            }
            return condition;
        }
    }
}
