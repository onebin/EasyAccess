using Demo.Model.EDMs;

namespace Demo.Repository.Bootstrap.EntityFramework.InitialData.Seed
{
    internal static class FormConfigSeed
    {
        public static FormConfig[] Values = new[]
            {
                new FormConfig
                    {
                        Id = 1,
                        Name = "测试",
                        Memo = "测试"
                    }
            };
    }
}