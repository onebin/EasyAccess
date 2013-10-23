using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using EasyAccess.Infrastructure.Repository;
using EasyAccess.Model.EDMs;
using EasyAccess.Repository.IRepositories;

namespace EasyAccess.Repository.Repositories
{
    public class RoleRepository : RepositoryBase<Role, long>, IRoleRepository
    {
        public virtual ICollection<Account> GetAccounts(long roleId)
        {
            var role = base.UnitOfWorkContext.Set<Role>()
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
            var role = base.UnitOfWorkContext.Set<Role>()
                .Include(x => x.Permissions)
                .SingleOrDefault(x => x.Id.Equals(roleId));
            if (role != null)
                return role.Permissions;
            return null;
        }

        public virtual ICollection<Permission> GetPermissions(long[] roleIds)
        {
            var roles = base.UnitOfWorkContext.Set<Role>()
                .Include(x => x.Permissions)
                .Where(x => roleIds.Contains(x.Id));
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

        public ICollection<Menu> GetMenus(long roleId)
        {
            var permissions = this.GetPermissions(roleId);
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
    }
}
