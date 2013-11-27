using Demo.Model.EDMs;

namespace Demo.Repository.Configurations.EntityFramework.InitializedData.Seed
{
    internal static class SectionConfigSeed
    {
        public static SectionConfig[] Values = new[]
            {
                new SectionConfig
                    {
                        Id = 1,
                        Name = "测试",
                        Article = ArticleConfigSeed.Values[0],
                        Index = 1,
                        IsRepeatable = false,
                        Depth = 1,
                        TreeFlag = "00001"
                    },
                new SectionConfig
                    {
                        Id = 2,
                        ParentId = 1,
                        Name = "文本",
                        Article = ArticleConfigSeed.Values[0],
                        Input = InputConfigSeed.Values[0],
                        Index = 1,
                        IsRepeatable = true,
                        Depth = 2,
                        TreeFlag = "0000100002"
                    },
                new SectionConfig
                    {
                        Id = 3,
                        ParentId = 1,
                        Name = "数字",
                        Article = ArticleConfigSeed.Values[0],
                        Input = InputConfigSeed.Values[1],
                        Index = 2,
                        IsRepeatable = true,
                        Depth = 2,
                        TreeFlag = "0000100003"
                    },
                new SectionConfig
                    {
                        Id = 4,
                        ParentId = 1,
                        Name = "下拉列表(单选)",
                        Article = ArticleConfigSeed.Values[0],
                        Input = InputConfigSeed.Values[2],
                        Index = 3,
                        IsRepeatable = true,
                        Depth = 2,
                        TreeFlag = "0000100004"
                    },
                new SectionConfig
                    {
                        Id = 5,
                        ParentId = 1,
                        Name = "下拉列表(多选)",
                        Article = ArticleConfigSeed.Values[0],
                        Input = InputConfigSeed.Values[3],
                        Index = 4,
                        IsRepeatable = true,
                        Depth = 2,
                        TreeFlag = "0000100005"
                    },
                new SectionConfig
                    {
                        Id = 6,
                        ParentId = 1,
                        Name = "日期",
                        Article = ArticleConfigSeed.Values[0],
                        Input = InputConfigSeed.Values[4],
                        Index = 5,
                        IsRepeatable = true,
                        Depth = 2,
                        TreeFlag = "0000100006"
                    },
                new SectionConfig
                    {
                        Id = 7,
                        ParentId = 1,
                        Name = "时间",
                        Article = ArticleConfigSeed.Values[0],
                        Input = InputConfigSeed.Values[5],
                        Index = 6,
                        IsRepeatable = true,
                        Depth = 2,
                        TreeFlag = "0000100007"
                    },
                new SectionConfig
                    {
                        Id = 8,
                        ParentId = 1,
                        Name = "日期时间",
                        Article = ArticleConfigSeed.Values[0],
                        Input = InputConfigSeed.Values[6],
                        Index = 7,
                        IsRepeatable = true,
                        Depth = 2,
                        TreeFlag = "0000100008"
                    },
                new SectionConfig
                    {
                         Id = 9,
                         ParentId = 1,
                         Name = "嵌套的控件",
                         Article = ArticleConfigSeed.Values[0],
                         Index = 8,
                         IsRepeatable = false,
                         Depth = 2,
                         TreeFlag = "0000100009"
                    },
                new SectionConfig
                    {
                         Id = 10,
                         ParentId = 9,
                         Name = "可重复的控件组1",
                         Article = ArticleConfigSeed.Values[0],
                         Index = 1,
                         IsRepeatable = true,
                         Depth = 3,
                         TreeFlag = "000010000900010"
                    },
                new SectionConfig
                    {
                         Id = 11,
                         ParentId = 10,
                         Name = "文本",
                         Article = ArticleConfigSeed.Values[0],
                         Input = InputConfigSeed.Values[7],
                         Index = 1,
                         IsRepeatable = false,
                         Depth = 4,
                         TreeFlag = "00001000090001000011"
                    },
                new SectionConfig
                    {
                         Id = 12,
                         ParentId = 10,
                         Name = "数字",
                         Article = ArticleConfigSeed.Values[0],
                         Input = InputConfigSeed.Values[8],
                         Index = 2,
                         IsRepeatable = false,
                         Depth = 4,
                         TreeFlag = "00001000090001000012"
                    },
                new SectionConfig
                    {
                         Id = 13,
                         ParentId = 9,
                         Name = "可重复的控件组2",
                         Article = ArticleConfigSeed.Values[0],
                         Index = 2,
                         IsRepeatable = true,
                         Depth = 3,
                         TreeFlag = "000010000900013"
                    },
                new SectionConfig
                    {
                         Id = 14,
                         ParentId = 13,
                         Name = "日期时间",
                         Article = ArticleConfigSeed.Values[0],
                         Input = InputConfigSeed.Values[11],
                         Index = 1,
                         IsRepeatable = false,
                         Depth = 4,
                         TreeFlag = "00001000090001300014"
                    },
                new SectionConfig
                    {
                         Id = 15,
                         ParentId = 13,
                         Name = "日期时间组",
                         Article = ArticleConfigSeed.Values[0],
                         Index = 2,
                         IsRepeatable = false,
                         Depth = 4,
                         TreeFlag = "00001000090001300015"
                    },
                new SectionConfig
                    {
                         Id = 16,
                         ParentId = 15,
                         Name = "日期",
                         Article = ArticleConfigSeed.Values[0],
                         Input = InputConfigSeed.Values[9],
                         Index = 1,
                         IsRepeatable = false,
                         Depth = 5,
                         TreeFlag = "0000100009000130001500016"
                    },
                new SectionConfig
                    {
                         Id = 17,
                         ParentId = 15,
                         Name = "时间",
                         Article = ArticleConfigSeed.Values[0],
                         Input = InputConfigSeed.Values[10],
                         Index = 2,
                         IsRepeatable = false,
                         Depth = 5,
                         TreeFlag = "0000100009000130001500017"
                    },
                new SectionConfig
                    {
                         Id = 18,
                         ParentId = 9,
                         Name = "不可重复的控件组3",
                         Article = ArticleConfigSeed.Values[0],
                         Index = 3,
                         IsRepeatable = false,
                         Depth = 3,
                         TreeFlag = "000010000900018"
                    },
                new SectionConfig
                    {
                         Id = 19,
                         ParentId = 18,
                         Name = "下拉列表(单选)",
                         Article = ArticleConfigSeed.Values[0],
                         Input = InputConfigSeed.Values[12],
                         Index = 1,
                         IsRepeatable = false,
                         Depth = 5,
                         TreeFlag = "0000100009000130001800019"
                    },
                new SectionConfig
                    {
                         Id = 20,
                         ParentId = 18,
                         Name = "下拉列表(多选)",
                         Article = ArticleConfigSeed.Values[0],
                         Input = InputConfigSeed.Values[13],
                         Index = 2,
                         IsRepeatable = false,
                         Depth = 5,
                         TreeFlag = "0000100009000130001800020"
                    }
            };
    }
}