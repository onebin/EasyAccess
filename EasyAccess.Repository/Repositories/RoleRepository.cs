using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using EasyAccess.Infrastructure.Repository;
using EasyAccess.Model.EDMs;
using EasyAccess.Repository.IRepositories;

namespace EasyAccess.Repository.Repositories
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
    }
}
