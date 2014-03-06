using System.Data.Entity.Infrastructure;
using EasyAccess.Infrastructure.Util.CustomTimestamp.Dialect;

namespace EasyAccess.Infrastructure.Util.CustomTimestamp
{
    public static class SqlCommandBuilder
    {
        public static ISqlCommand Build(DbEntityEntry entry)
        {
            return new MsSql(entry);
        }
    }
}