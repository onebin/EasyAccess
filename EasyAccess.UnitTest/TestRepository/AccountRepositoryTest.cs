using System.Collections.Generic;
using System.Linq;
using EasyAccess.Model.EDMs;
using EasyAccess.Repository.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EasyAccess.UnitTest.TestRepository
{
    [TestClass]
    public class AccountRepositoryTest
    {
        [TestMethod]
        public void TestGetRoles()
        {
            List<Role> roles = new List<Role>
                {
                    new Role() { Id = 1, Name = "管理员", Permissions = new List<Permission>
                        {
                            new Permission() { Id = "M01P01", Name = "浏览"},
                            new Permission() { Id = "M01P0101", Name = "添加"},
                            new Permission() { Id = "M01P0102", Name = "修改"},
                            new Permission() { Id = "M01P0103", Name = "删除"},
                        }},
                    new Role() { Id = 2, Name = "游客", Permissions = new List<Permission>
                        {
                            new Permission() { Id = "M01P01", Name = "浏览"},
                            new Permission() { Id = "M01P0104", Name = "打印"}
                        }},
                };
            var accountRepositoryMock = new Mock<AccountRepository>(null);
            accountRepositoryMock.Setup(x => x.GetRoles(It.IsAny<int>())).Returns(roles);
            var permissions = accountRepositoryMock.Object.GetPermissions(1);

            Assert.IsNotNull(permissions);
            Assert.AreEqual(5, permissions.Count);
            Assert.AreEqual(string.Join(",", new string[] { "M01P01", "M01P0101", "M01P0102", "M01P0103", "M01P0104" }), 
                string.Join(",", permissions.Select(x => x.Id)));

        }
    }
}
