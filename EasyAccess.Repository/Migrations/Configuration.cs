using System.Collections.Generic;
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

            var roles = new List<Role> { new Role() { Id = 1, Name = "¹ÜÀíÔ±", IsEnabled = true } };

            var account = new Account()
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
                Register = new Register
                {
                    LoginUser = new LoginUser
                    {
                        UserName = "Admin",
                        Password = "kLD+u1gdwB2ddmp6OmPZFXsQfcmsTUeyhyguDMpLsEM="
                    },
                    Salt = Guid.Parse("1A0AEF62-A8ED-44A3-93C9-48E9F9774B84")
                },
                CreateTime = DateTime.Now,
                Roles = roles
            };

            context.Accounts.AddOrUpdate(x => x.Id, account);
        }
    }
}
