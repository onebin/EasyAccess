using System.Data.Entity;
using Demo.Repository.Configurations.EntityFramework.InitialData;
using Demo.Repository.Migrations;

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
                DataInitializer.Initialize(context);
            }
        }

        class ResetAllDataIfModelChanges : DropCreateDatabaseIfModelChanges<DemoContext>
        {
            protected override void Seed(DemoContext context)
            {
                DataInitializer.Initialize(context);
            }
        }
    }
}
