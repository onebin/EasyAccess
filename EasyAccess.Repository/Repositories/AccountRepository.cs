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
using EasyAccess.Model.DTOs;
using EasyAccess.Model.EDMs;
using EasyAccess.Model.VOs;
using EasyAccess.Repository.IRepositories;

namespace EasyAccess.Repository.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {

        public ICollection<Role> GetRoles(long accountId)
        {
            var account = Entities
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
            var permissionLstInLst = (from a in UnitOfWorkContext.Set<Account>()
                                  from r in a.Roles
                                  where a.Id.Equals(accountId)
                                  select r.Permissions).ToList();
            ICollection<Permission> permissions = new Collection<Permission>();
            foreach (var permissionLst in permissionLstInLst)
            {
                foreach (var permission in permissionLst)
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
            var menus = (from a in Entities
                                           from r in a.Roles
                                           from p in r.Permissions
                                           where a.Id.Equals(accountId)
                                           select p.Menu
                                          ).Distinct().ToList();
            return menus;
        }


        public Register GetRegister(string userName)
        {
            var account = Entities
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
