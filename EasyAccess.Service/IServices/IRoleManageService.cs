using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyAccess.Infrastructure.Util.ConditionBuilder;
using EasyAccess.Infrastructure.Util.PagingData;
using EasyAccess.Model.EDMs;

namespace EasyAccess.Service.IServices
{
    public interface IRoleManageService
    {
        PagingData<Role> GetRolePagingData(PagingCondition pagingCondition, IQueryCondition<Role> queryCondition = null);
    }
}
