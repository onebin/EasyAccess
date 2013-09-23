using System.Collections.Generic;
using System.Data.Entity;
using EasyAccess.Model.EDMs;

namespace EasyAccess.Repository.Configuration
{
    public class EasyAccessInitializer : DropCreateDatabaseIfModelChanges<EasyAccessContext>
    {
        protected override void Seed(EasyAccessContext context)
        {
            var menus = new List<Menu>
            {
                new Menu() { Id = "M01", Name = "系统设置", Index = 1, System = "M", Url = "", Depth = 0 },
                new Menu() { Id = "M0101", Name = "用户管理", Index = 1, System = "M", Url = "SystemSettings/AccountManage/Index", Depth = 1 },
                new Menu() { Id = "M0102", Name = "角色管理", Index = 2, System = "M", Url = "SystemSettings/RoleManage/Index", Depth = 1 }
            };
            menus.ForEach(x => context.Menus.Add(x));
            context.SaveChanges();

            menus[1].ParentMenu = menus[0];
            menus[2].ParentMenu = menus[0];
            context.SaveChanges();

            var accounts = new List<Account>
            {
                new Account() { FirstName = "Ebin", LastName = "Wu" }
            };
            accounts.ForEach(x => context.Accounts.Add(x));
            context.SaveChanges();
        }
    }
}
