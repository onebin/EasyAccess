using EasyAccess.Infrastructure.Util.ConditionBuilder;
using EasyAccess.Infrastructure.Util.PagingData;
using EasyAccess.Model.DTOs;
using EasyAccess.Model.EDMs;

namespace EasyAccess.Service.IServices
{
    public interface IAccountManageService
    {
        PagingData<AccountDto> GetAccountPagingData(IQueryCondition<Account> queryCondition, PagingCondition pagingCondition);
    }
}
