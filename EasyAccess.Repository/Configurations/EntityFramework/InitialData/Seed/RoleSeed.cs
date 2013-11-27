using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyAccess.Model.EDMs;

namespace EasyAccess.Repository.Configurations.EntityFramework.Seed
{
    internal static class RoleSeed
    {
        public static Role[] Values = new[]
            {
                new Role
                {
                    Id = 1, Name = "管理员", IsEnabled = true
                },
                new Role
                {
                    Id = 2, Name = "试用", IsEnabled = true
                }
            };
    }
}
