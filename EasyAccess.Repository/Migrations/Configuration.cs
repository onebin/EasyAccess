using System.Collections.Generic;
using EasyAccess.Model.DTOs;
using EasyAccess.Model.EDMs;
using EasyAccess.Model.VOs;
using EasyAccess.Repository.Configurations;
using EasyAccess.Repository.Configurations.EntityFramework;
using EasyAccess.Repository.Configurations.EntityFramework.Seed;

namespace EasyAccess.Repository.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EasyAccessContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(EasyAccessContext context)
        {
            context.Accounts.AddOrUpdate(x => x.Id, AccountSeed.Accounts);
            context.Registers.AddOrUpdate(x => x.Id, RegisterSeed.Registers);
        }
    }
}
