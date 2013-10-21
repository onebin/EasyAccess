using System.ComponentModel;
using System.Linq;
using EasyAccess.Infrastructure.Service;
using EasyAccess.Infrastructure.Util.ConditionBuilder;
using EasyAccess.Infrastructure.Util.PagingData;
using EasyAccess.Model.EDMs;
using EasyAccess.Repository.IRepositories;
using EasyAccess.Repository.Repositories;
using EasyAccess.Service.IServices;

namespace EasyAccess.Service.Services
{
    public class RoleManageService : ServiceBase, IRoleManageService
    {
        public IRoleRepository RoleRepository { get; set; }

        public PagingData<Role> GetRolePagingData(IQueryCondition<Role> queryCondition, PagingCondition pagingCondition)
        {
            return GetPagingData(RoleRepository, queryCondition, pagingCondition);
        }
    }
}
