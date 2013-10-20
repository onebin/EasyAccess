using System.ComponentModel;
using System.Linq;
using EasyAccess.Infrastructure.Service;
using EasyAccess.Infrastructure.Util.ConditionBuilder;
using EasyAccess.Infrastructure.Util.PagingData;
using EasyAccess.Model.EDMs;
using EasyAccess.Repository.IRepositories;
using EasyAccess.Service.IServices;

namespace EasyAccess.Service.Services
{
    public class AccountManageService : ServiceBase, IAccountManageService
    {
        public IAccountRepository AccountRepository { get; set; }

        public PagingData<Account> GetAccountPagingData(IQueryCondition<Account> queryCondition, PagingCondition pagingCondition)
        {
            return AccountRepository.GetPagingData(queryCondition, pagingCondition);
        }
    }
}
