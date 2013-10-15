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
            var query = AccountRepository.Entities.Where(queryCondition.Predicate);
            var recordCount = query.Count();
            var recordData = query.OrderBy(x => x.Id).Skip(pagingCondition.Skip).Take(pagingCondition.PageSize).ToList();
            var pageData = new PagingData<Account>(recordCount, pagingCondition, recordData);
            return pageData;
        }
    }
}
