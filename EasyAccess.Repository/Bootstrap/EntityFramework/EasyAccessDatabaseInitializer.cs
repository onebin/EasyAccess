using System.Data.Entity;
using EasyAccess.Repository.Bootstrap.EntityFramework.InitialData;

namespace EasyAccess.Repository.Bootstrap.EntityFramework
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
