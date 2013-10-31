using Demo.Model.EDMs;
using Demo.Model.VOs;

namespace Demo.Repository.Configurations.EntityFramework.Seed
{
    internal static class InputConfigSeed
    {
        public static InputConfig[] Inputs = new[]
            {
                new InputConfig
                    {
                        Id = 1,
                        IsRequired = false,
                        DefaultValue = "",
                        InputType = InputType.Input
                    },
                new InputConfig
                    {
                        Id = 2,
                        IsRequired = false,
                        DefaultValue = "",
                        InputType = InputType.Input
                    },
                new InputConfig
                    {
                        Id = 3,
                        IsRequired = false,
                        DefaultValue = "",
                        InputType = InputType.Input
                    },
                new InputConfig
                    {
                        Id = 4,
                        IsRequired = false,
                        DefaultValue = "",
                        InputType = InputType.Input
                    }
            };
    }
}