using System.Data.Entity.Migrations;
using EasyAccess.Repository.Bootstrap.EntityFramework.InitialData.Seed;

namespace EasyAccess.Repository.Bootstrap.EntityFramework.InitialData
{
    internal static class DataInitializer
    {
        public static void Initialize(EasyAccessContext context)
        {
            context.Accounts.AddOrUpdate(x => x.Id, AccountSeed.Values);
            context.Registers.AddOrUpdate(x => x.Id, RegisterSeed.Values);
            context.Tests.AddOrUpdate(x => x.Id, TestSeed.Values);
        }
    }
}
