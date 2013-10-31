using System.Data.Entity;

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
            }
        }

        class ResetAllDataIfModelChanges : DropCreateDatabaseIfModelChanges<DemoContext>
        {
            protected override void Seed(DemoContext context)
            {
            }
        }
    }
}
