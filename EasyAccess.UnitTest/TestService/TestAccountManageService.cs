using System;
using EasyAccess.Infrastructure.Util.ConditionBuilder;
using EasyAccess.Infrastructure.Util.PagingData;
using EasyAccess.Model.EDMs;
using EasyAccess.UnitTest.Configurations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyAccess.UnitTest.TestService
{
    [TestClass]
    public class TestAccountManageService : SpringTestBase
    {
        [TestMethod]
        public void TestGetAccountPagingData()
        {
            var conditionBuilder = ConditionBuilder<Account>.Create();
            conditionBuilder.OrderBy(x => x.Age);
            var pagingCondition = new PagingCondition(0, 15);
            AccountManageService.GetAccountPagingData(conditionBuilder, pagingCondition);
        }
    }
}
