using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using EasyAccess.Infrastructure.Repository;
using EasyAccess.Model.EDMs;

namespace EasyAccess.Repository.Repositories
{
    public interface IAccountRepository: IRepositoryBase<Account>
    {
        List<Role> GetRoles(int accountId);

        List<Permission> GetPermissions(int accountId);

        List<Menu> GetMenus(int accountId);
    }
}
