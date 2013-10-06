using System;
using System.Collections.Generic;
using System.Data.Entity;
using EasyAccess.Model.DTOs;
using EasyAccess.Model.EDMs;

namespace EasyAccess.Repository.Configuration
{
    public class EasyAccessInitializer : DropCreateDatabaseIfModelChanges<EasyAccessContext>
    {
        protected override void Seed(EasyAccessContext context)
        {
            var accounts = new List<Account>
                {
                    new Account()
                        {
                            FirstName = "Yibin", 
                            LastName = "Wu",
                            NickName = "Onebin",
                            Sex = 1,
                            IsEnabled = true,
                            IsDeleted = false,
                            Register = new Register
                                {
                                    LoginUser = new LoginUser
                                    {
                                      UserName  = "Admin",
                                      Password = "kLD+u1gdwB2ddmp6OmPZFXsQfcmsTUeyhyguDMpLsEM="
                                    },
                                    Salt = Guid.Parse("1A0AEF62-A8ED-44A3-93C9-48E9F9774B84")
                                }
                        }
                };
            var roles = new List<Role>
                {
                    new Role() { Name = "管理员", IsEnabled = true }
                };
            accounts.ForEach(x => context.Accounts.Add(x));
            roles.ForEach(x => context.Roles.Add(x));

            accounts[0].Roles = roles;

            context.SaveChanges();
        }
    }
}
