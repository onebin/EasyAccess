using System.Linq;
using EasyAccess.Infrastructure.Util.ConditionBuilder;
using EasyAccess.Model.EDMs;
using EasyAccess.Repository.Repositories;
using EasyAccess.UnitTest.SpringTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyAccess.UnitTest.TestInfrastructure.TestUtil
{
    [TestClass]
    public class TestConditionBuilder : SpringTestBase
    {

        [TestMethod]
        public void TestCreate()
        {
            var builder = ConditionBuilder.Create<Account>();
            var account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNotNull(account);
        }

        [TestMethod]
        public void TestEmpty()
        {
            var account = AccountRepository.Entities.FirstOrDefault(ConditionBuilder.Create<Account>().Empty);
            Assert.IsNotNull(account);
        }

        [TestMethod]
        public void TestEquals()
        {
            var builder = ConditionBuilder.Create<Account>();
            builder.Equals(x => x.IsDeleted, false);
            builder.Equals(x => x.Sex, 1); 
            var account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNotNull(account);
        }

        [TestMethod]
        public void TestLike()
        {
            var builder = ConditionBuilder.Create<Account>();
            builder.Like(x => x.Name.FirstName, "bin");
            var account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNotNull(account);
        }
    }
}
