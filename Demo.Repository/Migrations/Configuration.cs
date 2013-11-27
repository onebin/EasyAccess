

namespace Demo.Repository.Migrations
{
    using System.Data.Entity.Migrations;
    using Configurations.EntityFramework;
    using Configurations.EntityFramework.InitialData;

    internal sealed class Configuration : DbMigrationsConfiguration<Demo.Repository.Configurations.EntityFramework.DemoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DemoContext context)
        {
            DataInitializer.Initialize(context);
        }
    }
}
