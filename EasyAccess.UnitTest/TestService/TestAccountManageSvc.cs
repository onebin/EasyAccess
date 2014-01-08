using EasyAccess.Infrastructure.Util.ConditionBuilder;
using EasyAccess.Infrastructure.Util.PagingData;
using EasyAccess.Model.DTOs;
using EasyAccess.Model.EDMs;
using EasyAccess.UnitTest.Bootstrap;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyAccess.UnitTest.TestService
{
    [TestClass]
    public class TestAccountManageSvc : SpringTestBase
    {
        [TestMethod]
        public void TestGetAccountPagingData()
        {
            var conditionBuilder = ConditionBuilder<Account>.Create();
            conditionBuilder.OrderBy(x => x.Sex).ThenBy(x => x.CreateTime);
            var pagingCondition = new PagingCondition(0, 15);
            Account.GetPagingDtoData<AccountDto>(pagingCondition, conditionBuilder);
        }
    }
}
