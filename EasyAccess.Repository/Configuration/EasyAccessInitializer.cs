using System.Collections.Generic;
using System.Data.Entity;
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
                            LoginName = "admin", 
                            Password = "21232f297a57a5a743894a0e4a801fc3", 
                            Sex = 1,
                            IsEnabled = true,
                            IsDeleted = false
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
