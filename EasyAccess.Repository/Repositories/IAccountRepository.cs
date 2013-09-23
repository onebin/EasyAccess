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
        ICollection<Role> GetRoles(int accountId);

        ICollection<Permission> GetPermissions(int accountId);

        ICollection<Menu> GetMenus(int accountId);
    }
}
