using System;
using EasyAccess.UnitTest.Configurations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyAccess.UnitTest.TestRepository
{
    [TestClass]
    public class RoleRepositoryTest : SpringTestBase
    {
        [TestMethod]
        public void TestGetPermissions()
        {
            var permissions =  RoleRepository.GetPermissions(new long[] { 1, 2 });
            Assert.AreEqual(3, permissions.Count);
        }

        [TestMethod]
        public void TestGetMenu()
        {
            var menus = RoleRepository.GetMenus(1);
            Assert.AreEqual(3, menus.Count);
        }
    }
}
