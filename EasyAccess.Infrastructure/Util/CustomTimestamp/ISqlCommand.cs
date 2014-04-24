using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;

namespace EasyAccess.Infrastructure.Util.CustomTimestamp
{
    public interface ISqlCommand
    {
        DbCommand Update();
    }
}