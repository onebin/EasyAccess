using System.ComponentModel;
using System.Linq;
using EasyAccess.Infrastructure.Util.ConditionBuilder;
using EasyAccess.Model.DTOs;
using EasyAccess.Model.EDMs;
using EasyAccess.Repository.Repositories;
using EasyAccess.UnitTest.Configurations;
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
            builder.Equal(x => x.Sex, Sex.Male); 
            var account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNotNull(account);

            builder.Clear();
            builder.Equal(x => x.Sex, Sex.Female);
            account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNull(account);
        }


        [TestMethod]
        public void TestNotEquals()
        {
            var builder = ConditionBuilder<Account>.Create();
            builder.NotEqual(x => x.Sex, Sex.Female);
            var account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNotNull(account);

            builder.Clear();
            builder.NotEqual(x => x.Sex, Sex.Male);
            builder.NotEqual(x => x.Sex, Sex.Unknown);
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
            builder.Between(x => x.Age, 20, 30);
            var account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNotNull(account);

            builder.Clear();
            builder.Between(x => x.Age, 30, 50);
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


            //builder.Clear();
            //builder.Fuzzy(x => x.Age, "20-30");
            //account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            //Assert.IsNotNull(account);


            //builder.Clear();
            //builder.Fuzzy(x => x.Age, "10-20");
            //account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            //Assert.IsNull(account);
        }

        [TestMethod]
        public void TestGreaterThanOrEqual()
        {
            var builder = ConditionBuilder<Account>.Create();
            builder.GreaterThanOrEqual(x => x.Age, 24);
            var account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNotNull(account);

            builder.Clear();
            builder.GreaterThanOrEqual(x => x.Age, 25);
            account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNull(account);
        }


        [TestMethod]
        public void TestLessThanOrEqual()
        {
            var builder = ConditionBuilder<Account>.Create();
            builder.LessThanOrEqual(x => x.Age, 24);
            var account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNotNull(account);

            builder.Clear();
            builder.LessThanOrEqual(x => x.Age, 16);
            account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNull(account);
        }

        [TestMethod]
        public void TestGreaterThan()
        {
            var builder = ConditionBuilder<Account>.Create();
            builder.GreaterThan(x => x.Age, 23);
            var account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNotNull(account);

            builder.Clear();
            builder.GreaterThan(x => x.Age, 24);
            account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNull(account);
        }


        [TestMethod]
        public void TestLessThan()
        {
            var builder = ConditionBuilder<Account>.Create();
            builder.LessThan(x => x.Age, 25);
            var account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNotNull(account);

            builder.Clear();
            builder.LessThan(x => x.Age, 16);
            account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNull(account);
        }

        [TestMethod]
        public void TestOrderBy1()
        {
            var conditionBuilder = ConditionBuilder<Account>.Create();
            conditionBuilder.OrderBy(x => new { x.Sex, x.Age });
            Assert.AreEqual("Sex", conditionBuilder.KeySelectors.First().Key);
            Assert.AreEqual("Age", conditionBuilder.KeySelectors.Last().Key);
            Assert.AreEqual(ListSortDirection.Ascending, conditionBuilder.KeySelectors.First().Value);
            Assert.AreEqual(ListSortDirection.Ascending, conditionBuilder.KeySelectors.Last().Value);


            conditionBuilder.OrderBy(x => new { x.Sex, x.Age }, ListSortDirection.Descending);
            Assert.AreEqual(2, conditionBuilder.KeySelectors.Count);
            Assert.AreEqual("Sex", conditionBuilder.KeySelectors.First().Key);
            Assert.AreEqual("Age", conditionBuilder.KeySelectors.Last().Key);
            Assert.AreEqual(ListSortDirection.Descending, conditionBuilder.KeySelectors.First().Value);
            Assert.AreEqual(ListSortDirection.Descending, conditionBuilder.KeySelectors.Last().Value);
        }

        [TestMethod]
        public void TestOrderBy2()
        {
            var conditionBuilder = ConditionBuilder<Account>.Create();
            conditionBuilder.OrderBy(x => x.Sex);
            conditionBuilder.OrderBy(x => x.Age);
            Assert.AreEqual("Sex", conditionBuilder.KeySelectors.First().Key);
            Assert.AreEqual("Age", conditionBuilder.KeySelectors.Last().Key);
            Assert.AreEqual(ListSortDirection.Ascending, conditionBuilder.KeySelectors.First().Value);
            Assert.AreEqual(ListSortDirection.Ascending, conditionBuilder.KeySelectors.Last().Value);

            conditionBuilder.OrderBy(x => x.Sex, ListSortDirection.Descending);
            Assert.AreEqual(2, conditionBuilder.KeySelectors.Count);
            Assert.AreEqual("Sex", conditionBuilder.KeySelectors.First().Key);
            Assert.AreEqual(ListSortDirection.Descending, conditionBuilder.KeySelectors.First().Value);
        }


        [TestMethod]
        public void TestOrderBy3()
        {
            var conditionBuilder = ConditionBuilder<Account>.Create();
            conditionBuilder.OrderBy(ListSortDirection.Descending, x => x.Name.FirstName, x => x.Name.LastName);
            Assert.AreEqual("FirstName", conditionBuilder.KeySelectors.First().Key);
            Assert.AreEqual("LastName", conditionBuilder.KeySelectors.Last().Key);
        }
    }
}
