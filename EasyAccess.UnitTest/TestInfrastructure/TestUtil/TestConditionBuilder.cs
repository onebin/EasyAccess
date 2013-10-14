﻿using System.Linq;
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
        public void TestAnd()
        {
            var builder = new ConditionBuilder<Account>();
            builder.Equals(x => x.IsDeleted, false);
            builder.Equals(x => x.Sex, 1);
            Assert.IsNotNull(builder.Predicate);
        }

        [TestMethod]
        public void TestPredicateWithDefaultConstructor()
        {
            var builder = new ConditionBuilder<Account>();
            var account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNotNull(account);
        }

        [TestMethod]
        public void TestPredicateWithOtherConstructor()
        {
            var builder = new ConditionBuilder<Account>(x => x.IsDeleted == false && x.Sex == 1);
            var account = AccountRepository.Entities.FirstOrDefault(builder.Predicate);
            Assert.IsNotNull(account);
        }
    }
}