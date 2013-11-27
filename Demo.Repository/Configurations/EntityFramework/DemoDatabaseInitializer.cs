using System.Data.Entity;
using System.Data.Entity.Migrations;
using Demo.Repository.Configurations.EntityFramework.InitializedData;
using Demo.Repository.Configurations.EntityFramework.InitializedData.Seed;

namespace Demo.Repository.Configurations.EntityFramework
{
    public static class DemoDatabaseInitializer
    {
        public static void Initialize()
        {
            Database.SetInitializer(new ResetAllData());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<DemoContext, Configuration>());
        }

        class ResetAllData : DropCreateDatabaseAlways<DemoContext>
        {
            protected override void Seed(DemoContext context)
            {
                InitialSubject.Initialize(context);
            }
        }

        class ResetAllDataIfModelChanges : DropCreateDatabaseIfModelChanges<DemoContext>
        {
            protected override void Seed(DemoContext context)
            {
                InitialSubject.Initialize(context);
            }
        }
    }
}
