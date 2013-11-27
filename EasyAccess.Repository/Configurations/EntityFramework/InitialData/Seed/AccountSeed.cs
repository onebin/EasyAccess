using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyAccess.Model.EDMs;
using EasyAccess.Model.VOs;

namespace EasyAccess.Repository.Configurations.EntityFramework.Seed
{
    internal static class AccountSeed
    {
        public static Account[] Values = new[]
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
                        Register = RegisterSeed.Values[0],
                        CreateTime = DateTime.Now,
                        Roles = new List<Role> {RoleSeed.Values[0]}
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
                        Register = RegisterSeed.Values[1],
                        CreateTime = DateTime.Now,
                        Roles = new List<Role> {RoleSeed.Values[1]}
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
                        Register = RegisterSeed.Values[2],
                        CreateTime = DateTime.Now,
                        Roles = new List<Role> {RoleSeed.Values[1]}
                    }
            };
    }
}
