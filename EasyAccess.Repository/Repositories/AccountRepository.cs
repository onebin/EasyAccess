using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using EasyAccess.Infrastructure.Repository;
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
    }
}
