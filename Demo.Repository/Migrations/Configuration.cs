using Demo.Repository.Bootstrap.EntityFramework.InitialData;

namespace Demo.Repository.Migrations
{
    using System.Data.Entity.Migrations;
    using Bootstrap.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<DemoContext>
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
