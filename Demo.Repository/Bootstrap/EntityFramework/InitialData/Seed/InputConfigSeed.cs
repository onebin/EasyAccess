using Demo.Model.EDMs;
using Demo.Model.VOs;

namespace Demo.Repository.Bootstrap.EntityFramework.InitialData.Seed
{
    internal static class InputConfigSeed
    {
        public static InputConfig[] Values = new[]
            {
                new InputConfig //0
                    {
                        Id = 2,
                        IsRequired = true,
                        DefaultValue = "",
                        InputType = InputType.ValidateBox
                    },
                new InputConfig //1
                    {
                        Id = 3,
                        IsRequired = true,
                        DefaultValue = "",
                        InputType = InputType.NumberBox
                    },
                new InputConfig //2
                    {
                        Id = 4,
                        IsRequired = true,
                        DefaultValue = "无",
                        InputType = InputType.SingleComboBox,
                        Memo = "无,A,B,C,D,E,F"
                    },
                new InputConfig //3
                    {
                        Id = 5,
                        IsRequired = true,
                        DefaultValue = "无",
                        InputType = InputType.MultiComboBox,
                        Memo = "无,A,B,C,D,E,F"
                    },
                new InputConfig //4
                    {
                        Id = 6,
                        IsRequired = true,
                        DefaultValue = "",
                        InputType = InputType.DateBox
                    },
                new InputConfig //5
                    {
                        Id = 7,
                        IsRequired = true,
                        DefaultValue = "",
                        InputType = InputType.TimeSpinner
                    },
                new InputConfig //6
                    {
                        Id = 8,
                        IsRequired = true,
                        DefaultValue = "",
                        InputType = InputType.DateTimeBox
                    },
                new InputConfig //7
                    {
                        Id = 11,
                        IsRequired = true,
                        DefaultValue = "",
                        InputType = InputType.ValidateBox
                    },
                new InputConfig //8
                    {
                        Id = 12,
                        IsRequired = true,
                        DefaultValue = "",
                        InputType = InputType.NumberBox
                    },
                new InputConfig //9
                    {
                        Id = 16,
                        IsRequired = true,
                        DefaultValue = "",
                        InputType = InputType.DateBox
                    },
                new InputConfig //10
                    {
                        Id = 17,
                        IsRequired = true,
                        DefaultValue = "",
                        InputType = InputType.TimeSpinner
                    },
                new InputConfig //11
                    {
                        Id = 14,
                        IsRequired = true,
                        DefaultValue = "",
                        InputType = InputType.DateTimeBox
                    },
                new InputConfig //12
                    {
                        Id = 19,
                        IsRequired = true,
                        DefaultValue = "无",
                        InputType = InputType.SingleComboBox,
                        Memo = "无,A,B,C,D,E,F"
                    },
                new InputConfig //13
                    {
                        Id = 20,
                        IsRequired = true,
                        DefaultValue = "无",
                        InputType = InputType.MultiComboBox,
                        Memo = "无,A,B,C,D,E,F"
                    },
            };
    }
}