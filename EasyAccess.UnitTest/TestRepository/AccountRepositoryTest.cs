using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private static readonly ICollection<Menu> Menus = new Collection<Menu>
            {
                new Menu() { Id = "M01" },
                new Menu() { Id = "M02" }
            };


        private static readonly ICollection<Role> Roles = new Collection<Role>
                {
                    new Role() { Id = 1, Permissions = new List<Permission>
                        {
                            new Permission() { Id = "M01P01", Menu = Menus.First(), MenuId = Menus.First().Id },
                            new Permission() { Id = "M01P0101", Menu = Menus.First(), MenuId = Menus.First().Id},
                            new Permission() { Id = "M01P0102", Menu = Menus.First(), MenuId = Menus.First().Id},
                            new Permission() { Id = "M01P0103", Menu = Menus.First(), MenuId = Menus.First().Id},
                            new Permission() { Id = "M02P01", Menu = Menus.Last(), MenuId = Menus.Last().Id}
                        }},
                    new Role() { Id = 2, Permissions = new List<Permission>
                        {
                            new Permission() { Id = "M01P01", Menu = Menus.First(), MenuId = Menus.First().Id},
                            new Permission() { Id = "M01P0104", Menu = Menus.First(), MenuId = Menus.First().Id},
                            new Permission() { Id = "M02P01", Menu = Menus.Last(), MenuId = Menus.Last().Id}
                        }},
                };

        [TestMethod]
        public void TestGetRoles()
        {
            
        }

        [TestMethod]
        public void TestGetPermissions()
        {
            var accountRepositoryMock = new Mock<AccountRepository>(null);
            accountRepositoryMock.Setup(x => x.GetRoles(It.IsAny<int>())).Returns(Roles);
            var permissions = accountRepositoryMock.Object.GetPermissions(1);

            Assert.IsNotNull(permissions);
            Assert.AreEqual(6, permissions.Count);
            Assert.AreEqual(
                string.Join(",", new string[] { "M01P01", "M01P0101", "M01P0102", "M01P0103", "M02P01", "M01P0104" }), 
                string.Join(",", permissions.Select(x => x.Id)));
            
            //验证GetRoles的调用次数
            accountRepositoryMock.Verify(x => x.GetRoles(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void TestGetMenus()
        {
            var accountRepositoryMock = new Mock<AccountRepository>(null);
            accountRepositoryMock.Setup(x => x.GetRoles(It.IsAny<int>())).Returns(Roles);
            var menus = accountRepositoryMock.Object.GetMenus(1);
            Assert.IsNotNull(menus);
            Assert.AreEqual(2, menus.Count);
            Assert.AreEqual(
                string.Join(",", Menus.Select(x => x.Id)),
                string.Join(",", menus.Select(x => x.Id)));
        }
    }
}
