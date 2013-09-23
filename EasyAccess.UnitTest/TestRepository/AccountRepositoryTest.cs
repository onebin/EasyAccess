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
        private static readonly ICollection<Menu> _Menus = new Collection<Menu>
            {
                new Menu() { Id = "M01" },
                new Menu() { Id = "M02" }
            };

        private static readonly ICollection<Permission> _Permissions = new Collection<Permission>
            {
                new Permission() { Id = "M01P01", Menu = _Menus.First(), MenuId = _Menus.First().Id },
                new Permission() { Id = "M01P0101", Menu = _Menus.First(), MenuId = _Menus.First().Id },
                new Permission() { Id = "M01P0102", Menu = _Menus.First(), MenuId = _Menus.First().Id },
                new Permission() { Id = "M01P0103", Menu = _Menus.First(), MenuId = _Menus.First().Id },
                new Permission() { Id = "M01P0104", Menu = _Menus.First(), MenuId = _Menus.First().Id },
                new Permission() { Id = "M02P01", Menu = _Menus.Last(), MenuId = _Menus.Last().Id }
            }; 

        private static readonly ICollection<Role> _Roles = new Collection<Role>
                {
                    new Role() { Id = 1, Permissions = _Permissions.Where(x => x.Id != "M01P0104").ToList() },
                    new Role() { Id = 2, Permissions = _Permissions.Where(x => x.Id.Length < 8).ToList() },
                };

        [TestMethod]
        public void TestGetPermissions()
        {
            var accountRepositoryMock = new Mock<AccountRepository>(null);
            accountRepositoryMock.Setup(x => x.GetRoles(It.IsAny<int>())).Returns(_Roles);
            var permissions = accountRepositoryMock.Object.GetPermissions(1);

            Assert.IsNotNull(permissions);
            Assert.AreEqual(5, permissions.Count);
            Assert.AreEqual(
                string.Join(",", _Permissions.Where(x => x.Id != "M01P0104").Select(x => x.Id)), 
                string.Join(",", permissions.Select(x => x.Id)));
            
            accountRepositoryMock.Verify(x => x.GetRoles(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void TestGetMenus()
        {
            var accountRepositoryMock = new Mock<AccountRepository>(null);
            accountRepositoryMock.Setup(x => x.GetPermissions(It.IsAny<int>())).Returns(_Permissions);
            var menus = accountRepositoryMock.Object.GetMenus(1);
            Assert.IsNotNull(menus);
            Assert.AreEqual(2, menus.Count);
            Assert.AreEqual(
                string.Join(",", _Menus.Select(x => x.Id)),
                string.Join(",", menus.Select(x => x.Id)));

            accountRepositoryMock.Verify(x => x.GetPermissions(It.IsAny<int>()), Times.Once);
        }
    }
}
