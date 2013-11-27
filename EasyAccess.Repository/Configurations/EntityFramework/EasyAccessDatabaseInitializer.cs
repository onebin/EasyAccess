using System.Data.Entity;
using EasyAccess.Repository.Configurations.EntityFramework.InitialData;
using EasyAccess.Repository.Migrations;

namespace EasyAccess.Repository.Configurations.EntityFramework
{
    public static class EasyAccessDatabaseInitializer
    {
        public static void Initialize()
        {
            Database.SetInitializer(new ResetAllDataIfModelChanges());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<EasyAccessContext, Configuration>());
        }

        class ResetAllData : DropCreateDatabaseAlways<EasyAccessContext>
        {
            protected override void Seed(EasyAccessContext context)
            {
                DataInitializer.Initialize(context);
            }
        }

        class ResetAllDataIfModelChanges : DropCreateDatabaseIfModelChanges<EasyAccessContext>
        {
            protected override void Seed(EasyAccessContext context)
            {
                DataInitializer.Initialize(context);
            }
        }
    }
}
