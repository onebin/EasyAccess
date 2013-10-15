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
            var builder = ConditionBuilder<Account>.Create();
            var account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNotNull(account);
        }

        [TestMethod]
        public void TestEmpty()
        {
            var account = AccountRepository.Entities.FirstOrDefault(ConditionBuilder<Account>.Empty);
            Assert.IsNotNull(account);
        }

        [TestMethod]
        public void TestEquals()
        {
            var builder = ConditionBuilder<Account>.Create();
            builder.Equals(x => x.IsDeleted, false);
            builder.Equals(x => x.Sex, 1); 
            var account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNotNull(account);
        }


        [TestMethod]
        public void TestNotEquals()
        {
            var builder = ConditionBuilder<Account>.Create();
            builder.NotEquals(x => x.Sex, 0);
            var account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNotNull(account);
        }

        [TestMethod]
        public void TestLike()
        {
            var builder = ConditionBuilder<Account>.Create();
            builder.Like(x => x.Name.FirstName, "bin");
            var account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNotNull(account);
        }

        [TestMethod]
        public void TestBetween()
        {
            
        }

        [TestMethod]
        public void TestIn()
        {
            var builder = ConditionBuilder<Account>.Create();
            builder.In(x => x.Name.LastName, "Wu", "Chen");
            var account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNotNull(account);
        }
    }
}
