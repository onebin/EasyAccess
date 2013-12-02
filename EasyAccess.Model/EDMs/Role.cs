using System.Collections.Generic;
using System.Linq;
using EasyAccess.Infrastructure.Entity;

namespace EasyAccess.Model.EDMs
{
    public class Role : AggregateRootBase<Role, long>
    {
        #region 属性

        public string Name { get; set; }

        public string HomePage { get; set; }

        public string Memo { get; set; }

        public bool IsEnabled { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }

        #endregion

        #region 实例方法

        public ICollection<Menu> GetMenus()
        {
            var menus = (from r in Repository.Entities
                         from p in r.Permissions
                         where r.Id == this.Id
                         select p.Menu).Distinct().ToList();
            return menus;
        }

        #endregion

        #region 静态方法

        public static ICollection<Permission> GetPermissions(long[] roleIds)
        {
            var permissions = (from r in Repository.Entities
                               from p in r.Permissions
                               where roleIds.Contains(r.Id)
                               select p).Distinct().ToList();
            return permissions;
        }

        #endregion
    }
}
