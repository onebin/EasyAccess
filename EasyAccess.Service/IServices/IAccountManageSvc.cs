using EasyAccess.Infrastructure.Util.ConditionBuilder;
using EasyAccess.Infrastructure.Util.PagingData;
using EasyAccess.Model.DTOs;
using EasyAccess.Model.EDMs;

namespace EasyAccess.Service.IServices
{
    public interface IAccountManageSvc
    {
        PagingData<AccountDto> GetAccountPagingData(PagingCondition pagingCondition, IQueryCondition<Account> queryCondition = null);
    }
}
