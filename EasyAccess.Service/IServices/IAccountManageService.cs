using EasyAccess.Infrastructure.Util.ConditionBuilder;
using EasyAccess.Infrastructure.Util.PagingData;
using EasyAccess.Model.EDMs;

namespace EasyAccess.Service.IServices
{
    public interface IAccountManageService
    {
        PagingData<Account> GetAccountPagingData(IQueryCondition<Account> queryCondition, PagingCondition pagingCondition);
    }
}
