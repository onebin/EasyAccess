using System.Collections.Generic;
using EasyAccess.Infrastructure.Repository;
using EasyAccess.Model.EDMs;

namespace EasyAccess.Repository.IRepositories
{
    public interface IRoleRepository : IRepositoryBase<Role, long>
    {
        ICollection<Account> GetAccounts(long roleId);

        ICollection<Permission> GetPermissions(long roleId);

        ICollection<Permission> GetPermissions(long[] roleIds);

        ICollection<Menu> GetMenus(long roleId);
    }
}
