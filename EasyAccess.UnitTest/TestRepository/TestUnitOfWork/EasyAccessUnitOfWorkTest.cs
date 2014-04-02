using System;
using System.Data.Entity.Infrastructure;
using EasyAccess.Model.EDMs;
using EasyAccess.UnitTest.Bootstrap;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyAccess.UnitTest.TestUnitOfWork
{
    [TestClass]
    public class EasyAccessUnitOfWorkTest : SpringTestBase
    {
        [TestMethod]
        [ExpectedException(typeof(DbUpdateConcurrencyException))]
        public void TestTrans()
        {
            Role.Update(new Role()
            {
                Id = 1,
                Name = "管理员",
                Memo = "Test",
            }); 
            Role.Update(new Role()
            {
                Id = 3,
                Name = "试用",
                Memo = "Test",
            }); 
            Role.Update(new Role()
            {
                Id = 2,
                Name = "试用",
                Memo = "Test",
            });
            EasyAccessUnitOfWork.Commit();
            Assert.Fail("期望抛出DbUpdateConcurrencyException，但实际没有！");
        }
    }
}
