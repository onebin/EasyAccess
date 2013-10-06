using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Management.Instrumentation;
using System.Security.Cryptography;
using System.Text;
using EasyAccess.Infrastructure.Repository;
using EasyAccess.Model.DTOs;
using EasyAccess.Model.EDMs;
using EasyAccess.Repository.IRepositories;

namespace EasyAccess.Repository.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(DbContext dbcontext) : base(dbcontext) {}

        public virtual ICollection<Role> GetRoles(long accountId)
        {
            var account = base.DbContext.Set<Account>()
                              .Include(x => x.Roles)
                              .SingleOrDefault(x => x.Id.Equals(accountId));
            if (account != null)
                return
                    account
                        .Roles;
            return null;
        }

        public virtual ICollection<Permission> GetPermissions(long accountId)
        {
            var roles = this.GetRoles(accountId);
            ICollection<Permission> permissions = new Collection<Permission>();
            foreach (var role in roles)
            {
                foreach (var permission in role.Permissions)
                {
                    if (!permissions.ToLookup(x => x.Id).Contains(permission.Id))
                    {
                        permissions.Add(permission);
                    }
                }
            }
            return permissions;
        }

        public ICollection<Menu> GetMenus(long accountId)
        {
            var permissions = this.GetPermissions(accountId);
            ICollection<Menu > menus = new Collection<Menu>();
            foreach (var permission in permissions)
            {
                if (!menus.ToLookup(x => x.Id).Contains(permission.MenuId))
                {
                    menus.Add(permission.Menu);
                }
            }
            return menus;
        }


        public Register GetRegister(string userName)
        {
            var account = base.DbContext.Set<Account>()
                              .Include(x => x.Register)
                              .SingleOrDefault(x => x.Register.LoginUser.UserName.Equals(userName));
            return account != null ? account.Register : null;
        }

        public Account VerifyLogin(LoginUser loginUser)
        {
            var register = this.GetRegister(loginUser.UserName);
            if (register != null)
            {
                var salt = register.Salt;
                var passwordAndSaltBytes = Encoding.UTF8.GetBytes(loginUser.Password + salt);
                var hashBytes = new SHA256Managed().ComputeHash(passwordAndSaltBytes);
                var hashString = Convert.ToBase64String(hashBytes);
                if (hashString == register.LoginUser.Password)
                {
                    return register.Account;
                }
            }
            return null;
        }

        public void ResetPasswork(LoginUser loginUser)
        {
             var register = this.GetRegister(loginUser.UserName);
            if (register != null)
            {
                var salt = Guid.NewGuid();
                var passwordAndSaltBytes = Encoding.UTF8.GetBytes(loginUser.Password + salt);
                var hashBytes = new SHA256Managed().ComputeHash(passwordAndSaltBytes);

                register.Salt = salt;
                register.LoginUser.Password = Convert.ToBase64String(hashBytes);

                base.DbContext.SaveChanges();
            }
            else
            {
                throw new InstanceNotFoundException();
            }
        }
    }
}
