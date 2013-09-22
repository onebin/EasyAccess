using System.Data.Entity;
using EasyAccess.Infrastructure.Repository;
using EasyAccess.Models.EDMs;

namespace EasyAccess.Infrastructure.Repositories
{
    public class AccountRepository : RepositoryBase<Account>
    {
        public AccountRepository(DbContext dbcontext) : base(dbcontext) {}


    }
}
