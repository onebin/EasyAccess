using Demo.Repository.Configurations.EntityFramework;
using Demo.Repository.Configurations.EntityFramework.InitializedData;

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
            InitialSubject.Initialize(context);
        }
    }
}
