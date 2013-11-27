using Demo.Model.EDMs;

namespace Demo.Repository.Configurations.EntityFramework.InitialData.Seed
{
    internal static class ArticleConfigSeed
    {
        public static ArticleConfig[] Values = new[]
            {
                new ArticleConfig
                    {
                        Id = 1,
                        Name = "测试",
                        Index = 1
                    }
            };
    }
}