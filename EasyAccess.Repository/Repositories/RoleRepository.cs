using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using EasyAccess.Infrastructure.Repository;
using EasyAccess.Model.EDMs;
using EasyAccess.Repository.IRepositories;

namespace EasyAccess.Repository.Repositories
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public virtual ICollection<Account> GetAccounts(long roleId)
        {
            var role = Entities
                .Include(x => x.Accounts)
                .SingleOrDefault(x => x.Id.Equals(roleId));
            if (role != null)
                return
                    role
                        .Accounts;
            return null;
        }

        public virtual ICollection<Permission> GetPermissions(long roleId)
        {
            var role = Entities
                .Include(x => x.Permissions)
                .SingleOrDefault(x => x.Id.Equals(roleId));
            if (role != null)
                return role.Permissions;
            return null;
        }

        public virtual ICollection<Permission> GetPermissions(long[] roleIds)
        {
            var permissions = (from r in Entities
                    from p in r.Permissions
                    where roleIds.Contains(r.Id)
                    select p).Distinct().ToList();
            return permissions;
        }

        public ICollection<Menu> GetMenus(long roleId)
        {
            var menus = (from r in Entities
                        from p in r.Permissions
                        where r.Id == roleId
                        select p.Menu).Distinct().ToList();
            return menus;
        }
    }
}
