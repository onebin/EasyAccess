using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyAccess.Model.EDMs;
using EasyAccess.Model.VOs;

namespace EasyAccess.Repository.Bootstrap.EntityFramework.InitialData.Seed
{
    internal static class TestSeed
    {
        public static Test[] Values = new[]
        {
            new Test
            {
                Id = 1,
                NonNullableInt = 1,
                NonNullableDecimal = 1,
                NonNullableFloat = 1,
                NonNullableDouble = 1,
                NonNullableByte = 1,
                NonNullableString = "~!@#$%^&*()_+={}[]|\\?.,<>--！@#￥%……&*（）——《》？:\"''/*jhgfj*/\\*564\\*",
                NonNullableDateTime = DateTime.UtcNow,
                NonNullableSexEnum = Sex.Unknown
            },
            new Test
            {
                Id = 2,
                NonNullableInt = 2,
                NonNullableDecimal = 2,
                NonNullableFloat = 2,
                NonNullableDouble = 2,
                NonNullableByte = 2,
                NonNullableString = "",
                NonNullableDateTime = DateTime.UtcNow,
                NonNullableSexEnum = Sex.Male
            }
        };
    }
}
