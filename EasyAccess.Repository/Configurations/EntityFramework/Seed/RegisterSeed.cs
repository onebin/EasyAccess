using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyAccess.Model.EDMs;
using EasyAccess.Model.VOs;

namespace EasyAccess.Repository.Configurations.EntityFramework.Seed
{
    internal static class RegisterSeed
    {
        public static Register[] Registers = new[]
            {
                new Register
                {
                    Id = 1,
                    LoginUser = new LoginUser
                    {
                        UserName = "admin",
                        Password = "kLD+u1gdwB2ddmp6OmPZFXsQfcmsTUeyhyguDMpLsEM=" //123456
                    },
                    Salt = Guid.Parse("1A0AEF62-A8ED-44A3-93C9-48E9F9774B84")
                },
                new Register
                {
                    Id = 2,
                    LoginUser = new LoginUser
                    {
                        UserName = "guestA",
                        Password = "UMCVJZjMfZ7KpsCGvzKfqTXQM4pS9m/cW5gMi0X7/60=" //123456
                    },
                    Salt = Guid.Parse("A6ADE9CA-5220-47CB-B398-C7052D99964C")
                },
                new Register
                {
                    Id = 3,
                    LoginUser = new LoginUser
                    {
                        UserName = "guestB",
                        Password = "Bus4BHAJ87LZJcfk75H0ogwd3MJ0+YXky1ZQw5egG4c=" //123456
                    },
                    Salt = Guid.Parse("221D0784-F3AF-43D7-B718-A88910B47859")
                }
            };
    }
}
