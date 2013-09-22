using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EasyAccess.Infrastructure.Repository;
using EasyAccess.Model.EDMs;

namespace EasyAccess.Repository.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(DbContext dbcontext) : base(dbcontext) {}

        public virtual List<Role> GetRoles(int accountId)
        {
            var singleOrDefault = base.DbContext.Set<Account>()
                .Include(x => x.Roles)
                .SingleOrDefault(x => x.Id.Equals(accountId));
            if (singleOrDefault != null)
                return
                    singleOrDefault
                        .Roles.ToList();
        }

        public virtual List<Permission> GetPermissions(int accountId)
        {
            throw new System.NotImplementedException();
        }

        public virtual List<Menu> GetMenus(int accountId)
        {
            throw new System.NotImplementedException();
        }
    }
}
