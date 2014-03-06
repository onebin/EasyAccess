using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Management.Instrumentation;
using EasyAccess.Infrastructure.Attr;
using EasyAccess.Infrastructure.Entity;
using EasyAccess.Infrastructure.Util.CustomTimestamp;
using EasyAccess.Infrastructure.Util.Encryption;
using EasyAccess.Model.VOs;

namespace EasyAccess.Model.EDMs
{
    public class Account : AggregateRootBase<Account,long>, ISoftDelete
    {
        #region 属性

        public int Age { get; set; }

        public Sex Sex { get; set; }

        public string Memo { get; set; }

        public bool IsDeleted { get; set; }

        public Name Name { get; set; }

        public Contact Contact { get; set; }

        public DateTime CreateTime { get; set; }

        public virtual Register Register { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        [Column("_RowVersion")]
        [CustomTimestamp(CustomTimestampUpdateMode.Equal)]
        public int RowVersion { get; set; }

        #endregion

        #region 实例方法

        public ICollection<Permission> GetPermissions()
        {
            var permissionLstInLst = (from a in Repository.Entities
                                      from r in a.Roles
                                      where a.Id.Equals(this.Id)
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

        public ICollection<Menu> GetMenus()
        {
            var menus = (from a in Repository.Entities
                         from r in a.Roles
                         from p in r.Permissions
                         where a.Id.Equals(this.Id)
                         select p.Menu)
                         .Distinct().ToList();
            return menus;
        }

        #endregion

        #region 静态方法


        public static Register GetRegister(string userName)
        {
            var account = Repository.Entities.SingleOrDefault(x => x.Register.LoginUser.UserName.Equals(userName));
            return account != null ? account.Register : null;
        }

        public static Account VerifyLogin(LoginUser loginUser)
        {
            var register = GetRegister(loginUser.UserName);
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

        public static void ResetPasswork(LoginUser loginUser)
        {
            var register = GetRegister(loginUser.UserName);
            if (register != null)
            {
                var salt = Guid.NewGuid();
                register.Salt = salt;
                register.LoginUser.Password = HashFunctionEncryption.Encrypt(loginUser.Password, salt);
            }
            else
            {
                throw new InstanceNotFoundException();
            }
        }

        #endregion

    }
}
