using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure;

namespace EasyAccess.Infrastructure.Util.CustomTimestamp.Dialect
{
    internal class MySql : SqlCommandBase
    {
        public MySql(DbEntityEntry entry) : base(entry) { }

        public override DbCommand Update()
        {
            throw new NotImplementedException();
        }
    }
}
