using System;
using System.Data.Entity.Infrastructure;

namespace EasyAccess.Infrastructure.Util.CustomTimestamp.Dialect
{
    internal class MySql : SqlCommandBase
    {
        public MySql(DbEntityEntry entry) : base(entry) { }

        public override string Update()
        {
            throw new NotImplementedException();
        }
    }
}
