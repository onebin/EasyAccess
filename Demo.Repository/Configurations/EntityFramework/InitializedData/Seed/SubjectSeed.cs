using Demo.Model.EDMs;

namespace Demo.Repository.Configurations.EntityFramework.InitializedData.Seed
{
    internal static class SubjectSeed
    {
        public static Subject[] Values = new[]
            {
                new Subject
                    {
                        Id = 1,
                        Name = "测试",
                        Memo = "测试"
                    }
            };
    }
}