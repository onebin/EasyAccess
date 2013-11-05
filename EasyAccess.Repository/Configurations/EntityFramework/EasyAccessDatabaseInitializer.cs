using System.Data.Entity;
using System.Data.Entity.Migrations;
using EasyAccess.Repository.Configurations.EntityFramework.Seed;
using EasyAccess.Repository.Migrations;

namespace EasyAccess.Repository.Configurations.EntityFramework
{
    public static class EasyAccessDatabaseInitializer
    {
        public static void Initialize()
        {
            Database.SetInitializer(new ResetAllDataIfModelChanges());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<EasyAccessContext, Configuration>());
        }

        class ResetAllData : DropCreateDatabaseAlways<EasyAccessContext>
        {
            protected override void Seed(EasyAccessContext context)
            {
                context.Accounts.AddOrUpdate(x => x.Id, AccountSeed.Accounts);
                context.Registers.AddOrUpdate(x => x.Id, RegisterSeed.Registers);
            }
        }

        class ResetAllDataIfModelChanges : DropCreateDatabaseIfModelChanges<EasyAccessContext>
        {
            protected override void Seed(EasyAccessContext context)
            {
                context.Accounts.AddOrUpdate(x => x.Id, AccountSeed.Accounts);
                context.Registers.AddOrUpdate(x => x.Id, RegisterSeed.Registers);
            }
        }
    }
}
