using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using EasyAccess.Infrastructure.Repository;
using EasyAccess.Models;

namespace EasyAccess.Infrastructure.Repositories
{
    public class AccountRepository : RepositoryBase<Account>
    {
        public AccountRepository(DbContext dbcontext) : base(dbcontext) {}
    }
}
