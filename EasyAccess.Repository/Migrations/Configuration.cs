using System.Collections.Generic;
using EasyAccess.Model.Complex;
using EasyAccess.Model.DTOs;
using EasyAccess.Model.EDMs;
using EasyAccess.Repository.Configurations;

namespace EasyAccess.Repository.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EasyAccessContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(EasyAccessContext context)
        {

            var roles = new Role[]
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

            var registers = new Register[]
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

            var accounts= new Account[]
            {
                new Account
                {
                    Id = 1,
                    Name = new Name
                    {

                        FirstName = "Yibin",
                        LastName = "Wu",
                        NickName = "51b"
                    },
                    Age = 24,
                    Contact = new Contact
                    {
                        Email = "onebin.net@gmail.com",
                        Phone = "020-87654321"
                    },
                    Sex = Sex.Male,
                    IsDeleted = false,
                    Memo = "https://github.com/onebin/EasyAccess",
                    Register = registers[0],
                    CreateTime = DateTime.Now,
                    Roles = new List<Role> { roles[0] }
                },
                new Account
                {
                    Id = 2,
                    Name = new Name
                    {

                        FirstName = "GuestA",
                        LastName = "",
                        NickName = "GuestA"
                    },
                    Age = 18,
                    Contact = new Contact {},
                    Sex = Sex.Unknown,
                    IsDeleted = false,
                    Register = registers[1],
                    CreateTime = DateTime.Now,
                    Roles = new List<Role> { roles[1] }
                },
                new Account
                {
                    Id = 3,
                    Name = new Name
                    {

                        FirstName = "GuestB",
                        LastName = "",
                        NickName = "GuestB"
                    },
                    Age = 20,
                    Contact = new Contact {},
                    Sex = Sex.Unknown,
                    IsDeleted = false,
                    Register = registers[2],
                    CreateTime = DateTime.Now,
                    Roles = new List<Role> { roles[1] }
                }
            };

            context.Accounts.AddOrUpdate(x => x.Id, accounts);
            context.Registers.AddOrUpdate(x => x.Id, registers);

        }
    }
}
