﻿using EasyAccess.Infrastructure.Service;
using EasyAccess.Infrastructure.Util.ConditionBuilder;
using EasyAccess.Infrastructure.Util.PagingData;
using EasyAccess.Model.EDMs;
using EasyAccess.Service.IServices;

namespace EasyAccess.Service.Services
{
    public class RoleManageSvc : ServiceBase, IRoleManageSvc
    {
        public PagingData<Role> GetRolePagingData(PagingCondition pagingCondition, IQueryCondition<Role> queryCondition = null)
        {
            return Role.GetPagingEdmData(pagingCondition, queryCondition);
        }
    }
}
