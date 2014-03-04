using System;
using System.Collections.Generic;
using EasyAccess.Model.EDMs;
using EasyAccess.Model.VOs;

namespace EasyAccess.Repository.Bootstrap.EntityFramework.InitialData.Seed
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
                        Roles = new List<Role> {RoleSeed.Values[0]},
                        RowVersion = 0
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
                        Roles = new List<Role> {RoleSeed.Values[1]},
                        RowVersion = 0
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
                        Roles = new List<Role> {RoleSeed.Values[1]},
                        RowVersion = 0
                    }
            };
    }
}
