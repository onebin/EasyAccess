using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Management.Instrumentation;
using System.Security.Cryptography;
using System.Text;
using EasyAccess.Infrastructure.Repository;
using EasyAccess.Infrastructure.Util.Encryption;
using EasyAccess.Model.Complex;
using EasyAccess.Model.DTOs;
using EasyAccess.Model.EDMs;
using EasyAccess.Repository.IRepositories;

namespace EasyAccess.Repository.Repositories
{
    public class AccountRepository : RepositoryBase<Account, long>, IAccountRepository
    {

        public ICollection<Role> GetRoles(long accountId)
        {
            var account = base.UnitOfWorkContext.Set<Account,long>()
                              .Include(x => x.Roles)
                              .SingleOrDefault(x => x.Id.Equals(accountId));
            if (account != null)
                return
                    account
                        .Roles;
            return null;
        }

        public ICollection<Permission> GetPermissions(long accountId)
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
            var account = base.UnitOfWorkContext.Set<Account,long>()
                              .Include(x => x.Register)
                              .SingleOrDefault(x => x.Register.LoginUser.UserName.Equals(userName));
            return account != null ? account.Register : null;
        }

        public Account VerifyLogin(LoginUser loginUser)
        {
            var register = this.GetRegister(loginUser.UserName);
            if (register != null)
            {
                var hashString = HashFunctionEncryption.Encrypt(loginUser.Password, register.Salt);
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
                register.Salt = salt;
                register.LoginUser.Password = HashFunctionEncryption.Encrypt(loginUser.Password, salt);
                base.UnitOfWork.Commit();
            }
            else
            {
                throw new InstanceNotFoundException();
            }
        }
    }
}
