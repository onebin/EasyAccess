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
            var token = AuthorizationManager.GetInstance().GetToken(RoleLst);
            Assert.AreEqual("^%y7@&#l,581%fa)ft'rtq222a!4%}qwr]3^%y7@&#l,584", token);
        }
    }
}
