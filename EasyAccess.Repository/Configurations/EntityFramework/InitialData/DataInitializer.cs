using System.Data.Entity.Migrations;
using EasyAccess.Repository.Configurations.EntityFramework.Seed;

namespace EasyAccess.Repository.Configurations.EntityFramework.InitialData
{
    internal static class DataInitializer
    {
        public static void Initialize(EasyAccessContext context)
        {
            context.Accounts.AddOrUpdate(x => x.Id, AccountSeed.Values);
            context.Registers.AddOrUpdate(x => x.Id, RegisterSeed.Values);
        }
    }
}
