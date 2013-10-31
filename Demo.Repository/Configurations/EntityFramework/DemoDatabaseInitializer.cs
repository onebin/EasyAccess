using System.Data.Entity;
using System.Data.Entity.Migrations;
using Demo.Repository.Configurations.EntityFramework.Seed;

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
                context.ArticleConfigs.AddOrUpdate(x => x.Id, ArticleConfigSeed.Articles);
                context.SectionConfigs.AddOrUpdate(x => x.Id, SectionConfigSeed.Sections);
                context.InputConfigs.AddOrUpdate(x => x.Id, InputConfigSeed.Inputs);
            }
        }

        class ResetAllDataIfModelChanges : DropCreateDatabaseIfModelChanges<DemoContext>
        {
            protected override void Seed(DemoContext context)
            {
                context.ArticleConfigs.AddOrUpdate(x => x.Id, ArticleConfigSeed.Articles);
                context.SectionConfigs.AddOrUpdate(x => x.Id, SectionConfigSeed.Sections);
                context.InputConfigs.AddOrUpdate(x => x.Id, InputConfigSeed.Inputs);
            }
        }
    }
}
