using System;
using System.ComponentModel;
using System.Linq;
using EasyAccess.Infrastructure.Util.ConditionBuilder;
using EasyAccess.Model.DTOs;
using EasyAccess.Model.EDMs;
using EasyAccess.Model.VOs;
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

            builder.Clear();
            ;
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
        [ExpectedException(typeof(NotSupportedException))]
        public void TestOrderBy1()
        {
            var conditionBuilder = ConditionBuilder<Account>.Create();

            conditionBuilder.OrderBy(x => new { x.Sex, x.Age });

            Assert.Fail("期望抛出NotSupportedException，但实际并没抛出异常");
        }

        [TestMethod]
        public void TestOrderBy2()
        {
            var conditionBuilder = ConditionBuilder<Account>.Create();

            conditionBuilder.OrderBy(x => x.Sex);
            conditionBuilder.OrderByDescending(x => x.Age);

            Assert.AreEqual(2, conditionBuilder.OrderByConditions.Count);
            Assert.AreEqual("Sex", conditionBuilder.OrderByConditions.First().Key);
            Assert.AreEqual("Age", conditionBuilder.OrderByConditions.Last().Key);
            Assert.AreEqual(ListSortDirection.Ascending, conditionBuilder.OrderByConditions.First().Value.Direction);
            Assert.AreEqual(ListSortDirection.Descending, conditionBuilder.OrderByConditions.Last().Value.Direction);
        }

        [TestMethod]
        public void TestOrderBy3()
        {
            var conditionBuilder = ConditionBuilder<Account>.Create();

            conditionBuilder.OrderBy(x => x.Sex);
            conditionBuilder.OrderByDescending(x => x.Sex);

            Assert.AreEqual(1, conditionBuilder.OrderByConditions.Count);
            Assert.AreEqual("Sex", conditionBuilder.OrderByConditions.First().Key);
            Assert.AreEqual(ListSortDirection.Descending, conditionBuilder.OrderByConditions.Last().Value.Direction);
        }

        [TestMethod]
        public void TestOrderBy4()
        {
            var conditionBuilder = ConditionBuilder<Account>.Create();

            conditionBuilder.OrderBy(x => x.Sex).ThenByDescending(x => x.Id).ThenBy(x => x.Name.FirstName);
            Assert.AreEqual(3, conditionBuilder.OrderByConditions.Count);
            Assert.AreEqual("Sex", conditionBuilder.OrderByConditions.First().Key);
            Assert.AreEqual("FirstName", conditionBuilder.OrderByConditions.Last().Key);
            Assert.AreEqual(ListSortDirection.Ascending, conditionBuilder.OrderByConditions.First().Value.Direction);
            Assert.AreEqual(ListSortDirection.Descending, conditionBuilder.OrderByConditions.Single(x => x.Key == "Id").Value.Direction);
            Assert.AreEqual(ListSortDirection.Ascending, conditionBuilder.OrderByConditions.Last().Value.Direction);
        }

        [TestMethod]
        public void TestGetSoftDeletedItems()
        {
            AccountRepository.Delete(AccountRepository.Entities);

            var conditionBuilder = ConditionBuilder<Account>.Create();
            Assert.AreEqual(false, AccountRepository.Entities.Where(conditionBuilder.Predicate).Any());

            conditionBuilder.Clear();
            conditionBuilder.IsGetSoftDeletedItems = true;
            Assert.AreEqual(true, AccountRepository.Entities.Where(conditionBuilder.Predicate).Any());
        }

    }
}
