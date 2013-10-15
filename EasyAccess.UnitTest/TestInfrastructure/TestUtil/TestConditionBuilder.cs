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
            builder.Equal(x => x.IsDeleted, false);
            builder.Equal(x => x.Sex, 1); 
            var account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNotNull(account);

            builder.Clear();
            builder.Equal(x => x.Sex, 0);
            account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNull(account);
        }


        [TestMethod]
        public void TestNotEquals()
        {
            var builder = ConditionBuilder<Account>.Create();
            builder.NotEqual(x => x.Sex, 0);
            var account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNotNull(account);

            builder.Clear();
            builder.NotEqual(x => x.Sex, 1);
            account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNull(account);
        }

        [TestMethod]
        public void TestLike()
        {
            var builder = ConditionBuilder<Account>.Create();
            builder.Like(x => x.Name.FirstName, "bin");
            var account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNotNull(account);

            builder.Clear(); ;
            builder.Like(x => x.Name.FirstName, "51b");
            account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNull(account);
        }

        [TestMethod]
        public void TestBetween()
        {
            var builder = ConditionBuilder<Account>.Create();
            builder.Between(x => x.Sex, 0, 1);
            var account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNotNull(account);

            builder.Clear();
            builder.Between(x => x.Sex, 2, 3);
            account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNull(account);
        }

        [TestMethod]
        public void TestIn()
        {
            var builder = ConditionBuilder<Account>.Create();
            builder.In(x => x.Name.LastName, "Wu", "Chen");
            var account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNotNull(account);

            builder.Clear();
            builder.In(x => x.Name.LastName, "hello", "world");
            account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNull(account);
        }

        [TestMethod]
        public void TestFuzzy()
        {
            var builder = ConditionBuilder<Account>.Create();
            builder.Fuzzy(x => x.Name.LastName, "yi,bin,yibin,Wu");
            var account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNotNull(account);

            builder.Clear();
            builder.Fuzzy(x => x.Name.LastName, "world,good");
            account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNull(account);


            builder.Clear();
            builder.Fuzzy(x => x.Sex, "0-1");
            account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNotNull(account);


            builder.Clear();
            builder.Fuzzy(x => x.Sex, "2-3");
            account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNull(account);
        }

        [TestMethod]
        public void TestGreaterThanOrEqual()
        {
            var builder = ConditionBuilder<Account>.Create();
            builder.GreaterThanOrEqual(x => x.Sex, 1);
            var account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNotNull(account);

            builder.Clear();
            builder.GreaterThanOrEqual(x => x.Sex, 2);
            account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNull(account);
        }


        [TestMethod]
        public void TestLessThanOrEqual()
        {
            var builder = ConditionBuilder<Account>.Create();
            builder.LessThanOrEqual(x => x.Sex, 1);
            var account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNotNull(account);

            builder.Clear();
            builder.LessThanOrEqual(x => x.Sex, 0);
            account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNull(account);
        }

        [TestMethod]
        public void TestGreaterThan()
        {
            var builder = ConditionBuilder<Account>.Create();
            builder.GreaterThan(x => x.Sex, 0);
            var account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNotNull(account);

            builder.Clear();
            builder.GreaterThan(x => x.Sex, 1);
            account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNull(account);
        }


        [TestMethod]
        public void TestLessThan()
        {
            var builder = ConditionBuilder<Account>.Create();
            builder.LessThan(x => x.Sex, 2);
            var account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNotNull(account);

            builder.Clear();
            builder.LessThan(x => x.Sex, 1);
            account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNull(account);
        }
    }
}
