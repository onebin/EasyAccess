using System.Data.Entity.Migrations;
using Demo.Repository.Bootstrap.EntityFramework.InitialData.Seed;

namespace Demo.Repository.Bootstrap.EntityFramework.InitialData
{
    internal static class DataInitializer
    {
        public static void Initialize(DemoContext context)
        {
            context.Subjects.AddOrUpdate(x => x.Id, FormConfigSeed.Values);
            context.ArticleConfigs.AddOrUpdate(x => x.Id, ArticleConfigSeed.Values);
            context.InputConfigs.AddOrUpdate(x => x.Id, InputConfigSeed.Values);
            context.SectionConfigs.AddOrUpdate(x => x.Id, SectionConfigSeed.Values);
        }
    }
}
