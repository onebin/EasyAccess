using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Collections;

namespace EasyAccess.Infrastructure.Util.Sql
{
    public class MsSql : ISqlCommand
    {
        public string Update(string tableName, Dictionary<string, string> columnNameAndValues, string id)
        {
            var sets = columnNameAndValues.Select(x => "[" + x.Key + "] = " + "'" + x.Value + "'");
            return "UPDATE [" + tableName + "] SET " + string.Join(", ", sets) + " WHERE [Id] = '" + id + "'; ";
        }
    }
}
