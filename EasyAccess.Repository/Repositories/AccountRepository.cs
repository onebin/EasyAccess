﻿using System.Data.Entity;
using EasyAccess.Infrastructure.Repository;
using EasyAccess.Model.EDMs;

namespace EasyAccess.Repository.Repositories
{
    public class AccountRepository : RepositoryBase<Account>
    {
        public AccountRepository(DbContext dbcontext) : base(dbcontext) {}


    }
}
