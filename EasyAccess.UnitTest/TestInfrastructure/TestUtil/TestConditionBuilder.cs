using EasyAccess.Infrastructure.Util.ConditionBuilder;
using EasyAccess.Model.EDMs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyAccess.UnitTest.TestInfrastructure.TestUtil
{
    [TestClass]
    public class TestConditionBuilder
    {
        [TestMethod]
        public void TestAnd()
        {
            var builder = new ConditionBuilder<Account>();
            builder.And(x => x.IsDeleted == false);
            builder.And(x => x.Name.ToString() == "WuYibin");
            builder.And(x => x.Sex == 1);
            Assert.IsNotNull(builder.Predicate);
        }
    }
}
