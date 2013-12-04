using System;
using EasyAccess.Model.EDMs;
using EasyAccess.UnitTest.Bootstrap;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyAccess.UnitTest.TestRepository
{
    [TestClass]
    public class RoleRepositoryTest : SpringTestBase
    {
        [TestMethod]
        public void TestGetPermissions()
        {
            var permissions =  Role.GetPermissions(new long[] { 1, 2 });
            Assert.AreEqual(3, permissions.Count);
        }

        [TestMethod]
        public void TestGetMenu()
        {
            var menus = Role.FindById(1).GetMenus();
            Assert.AreEqual(3, menus.Count);
        }
    }
}
