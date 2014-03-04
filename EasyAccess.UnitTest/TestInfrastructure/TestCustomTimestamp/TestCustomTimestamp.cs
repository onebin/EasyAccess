using System;
using EasyAccess.Model.EDMs;
using EasyAccess.UnitTest.Bootstrap;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyAccess.UnitTest.TestInfrastructure.TestCustomTimestamp
{
    [TestClass]
    public class TestCustomTimestamp: SpringTestBase
    {
        [TestMethod]
        public void TestUpdateMode_Equal()
        {
            var account = Account.FindById(1);
            account.Age = 25;
            Account.Update(account);
            EasyAccessUnitOfWork.Commit();

            Assert.AreEqual(25, account.Age);
        }
    }
}
