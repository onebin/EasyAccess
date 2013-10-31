using Demo.Model.EDMs;

namespace Demo.Repository.Configurations.EntityFramework.Seed
{
    internal static class ArticleConfigSeed
    {
        public static ArticleConfig[] Articles = new[]
            {
                new ArticleConfig
                    {
                        Id = 1,
                        Name = "基本资料",
                        Index = 1
                    },
                new ArticleConfig
                    {
                        Id = 2,
                        Name = "成本信息",
                        Index = 2
                    },
                new ArticleConfig
                    {
                        Id = 3,
                        Name = "操作流程",
                        Index = 3
                    },
                new ArticleConfig
                    {
                        Id = 4,
                        Name = "技术能力",
                        Index = 4
                    }
            };
    }
}