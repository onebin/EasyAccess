using Demo.Model.EDMs;

namespace Demo.Repository.Configurations.EntityFramework.Seed
{
    internal static class SectionConfigSeed
    {
        public static SectionConfig[] Sections = new[]
            {
                new SectionConfig
                    {
                        Id = 1,
                        Name = "联系人",
                        Article = ArticleConfigSeed.Articles[0],
                        Index = 1,
                        IsRepeatable = false,
                        Depth = 1
                    },
                new SectionConfig
                    {
                        Id = 2,
                        ParentId = 1,
                        Name = "销售代表",
                        Article = ArticleConfigSeed.Articles[0],
                        Input = InputConfigSeed.Inputs[0],
                        Index = 1,
                        IsRepeatable = true,
                        Depth = 2
                    },
                new SectionConfig
                    {
                        Id = 3,
                        ParentId = 1,
                        Name = "客服代表",
                        Article = ArticleConfigSeed.Articles[0],
                        Input = InputConfigSeed.Inputs[1],
                        Index = 2,
                        IsRepeatable = true,
                        Depth = 2
                    },
                new SectionConfig
                    {
                        Id = 4,
                        ParentId = 1,
                        Name = "财务人员",
                        Article = ArticleConfigSeed.Articles[0],
                        Input = InputConfigSeed.Inputs[2],
                        Index = 3,
                        IsRepeatable = true,
                        Depth = 2
                    },
                new SectionConfig
                    {
                        Id = 5,
                        ParentId = 1,
                        Name = "IT部人员",
                        Article = ArticleConfigSeed.Articles[0],
                        Input = InputConfigSeed.Inputs[3],
                        Index = 4,
                        IsRepeatable = true,
                        Depth = 2
                    }
            };
    }
}