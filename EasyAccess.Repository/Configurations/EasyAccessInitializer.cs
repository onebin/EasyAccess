using System.Data.Entity;
using EasyAccess.Repository.Migrations;

namespace EasyAccess.Repository.Configurations
{
    public class EasyAccessInitializer
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EasyAccessContext, Configuration>());
        }
    }
}
