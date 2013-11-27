namespace EasyAccess.Repository.Migrations
{
    using System.Data.Entity.Migrations;
    using Configurations.EntityFramework;
    using Configurations.EntityFramework.InitialData;

    internal sealed class Configuration : DbMigrationsConfiguration<EasyAccessContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(EasyAccessContext context)
        {
            DataInitializer.Initialize(context);
        }
    }
}
