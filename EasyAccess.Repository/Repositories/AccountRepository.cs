using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Management.Instrumentation;
using System.Security.Cryptography;
using System.Text;
using EasyAccess.Infrastructure.Repository;
using EasyAccess.Infrastructure.Util.Encryption;
using EasyAccess.Model.DTOs;
using EasyAccess.Model.EDMs;
using EasyAccess.Model.VOs;
using EasyAccess.Repository.IRepositories;

namespace EasyAccess.Repository.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
    }
}
