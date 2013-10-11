using System;
using System.Collections.Generic;
using EasyAccess.Infrastructure.Authorization;
using EasyAccess.Model.EDMs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyAccess.UnitTest.TestAuthorization
{
    [TestClass]
    public class AuthorizationManagerTest
    {
        private static readonly List<Role> RoleLst = new List<Role>
        {
            new Role(){ Id = 3},
            new Role() {Id = 4},
            new Role() {Id = 2},
            new Role() {Id = 1}
        };

        [TestMethod]
        public void TestGetToken()
        {
            var token = AuthorizationManager.GetInstance().GetToken(RoleLst, null);
            Assert.AreEqual("03c0e92537cba41e087e0d4937f04db1", token);
        }

    }
}
