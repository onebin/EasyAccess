using System.Collections.Generic;

namespace EasyAccess.Infrastructure.Util.Sql
{
    public interface ISqlCommand
    {
        string Update(string tableName, Dictionary<string, string> columnNameAndValues, string id);
    }
}