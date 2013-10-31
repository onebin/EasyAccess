using Demo.Repository.Configurations.EntityFramework;
using Demo.Repository.Configurations.EntityFramework.Seed;

namespace Demo.Repository.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Demo.Repository.Configurations.EntityFramework.DemoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DemoContext context)
        {
            context.ArticleConfigs.AddOrUpdate(x => x.Id, ArticleConfigSeed.Articles);
            context.SectionConfigs.AddOrUpdate(x => x.Id, SectionConfigSeed.Sections);
            context.InputConfigs.AddOrUpdate(x => x.Id, InputConfigSeed.Inputs);
        }
    }
}
