using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EasyAccess.Model.EDMs;
using EasyAccess.Model.VOs;
using EasyAccess.UnitTest.Configurations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyAccess.UnitTest.TestRepository
{
    [TestClass]
    public class AccountRepositoryTest : SpringTestBase
    {
        private static readonly ICollection<Menu> Menus = new Collection<Menu>
            {
                new Menu() {Id = "M01"},
                new Menu() {Id = "M02"}
            };

        private static readonly ICollection<Permission> Permissions = new Collection<Permission>
            {
                new Permission() {Id = "M01P01", Menu = Menus.First(), MenuId = Menus.First().Id},
                new Permission() {Id = "M01P0101", Menu = Menus.First(), MenuId = Menus.First().Id},
                new Permission() {Id = "M01P0102", Menu = Menus.First(), MenuId = Menus.First().Id},
                new Permission() {Id = "M01P0103", Menu = Menus.First(), MenuId = Menus.First().Id},
                new Permission() {Id = "M01P0104", Menu = Menus.First(), MenuId = Menus.First().Id},
                new Permission() {Id = "M02P01", Menu = Menus.Last(), MenuId = Menus.Last().Id}
            };

        private static readonly ICollection<Role> Roles = new Collection<Role>
            {
                new Role() {Id = 1, Permissions = Permissions.Where(x => x.Id != "M01P0104").ToList()},
                new Role() {Id = 2, Permissions = Permissions.Where(x => x.Id.Length < 8).ToList()},
            };

        //[TestMethod]
        //public void TestGetPermissions()
        //{
        //    var accountRepositoryMock = new Mock<AccountRepository>();
        //    accountRepositoryMock.SetupProperty(x => x.UnitOfWork, new EasyAccessUnitOfWork(new EasyAccessContext()));
        //    //GetPermissions -virtual ， GetRoles +virtual
        //    accountRepositoryMock.Setup(x => x.GetRoles(It.IsAny<long>())).Returns(Roles);
        //    var permissions = accountRepositoryMock.Object.GetPermissions(1);

        //    Assert.IsNotNull(permissions);
        //    Assert.AreEqual(5, permissions.Count);
        //    Assert.AreEqual(
        //        string.Join(",", Permissions.Where(x => x.Id != "M01P0104").Select(x => x.Id)), 
        //        string.Join(",", permissions.Select(x => x.Id)));

        //    accountRepositoryMock.Verify(x => x.GetRoles(It.IsAny<long>()), Times.Once);
        //}

        [TestMethod]
        public void TestGetPermissions()
        {
            var permissions = AccountRepository.GetPermissions(1);
            Assert.AreEqual(3, permissions.Count);
        }

        //[TestMethod]
        //public void TestGetMenus()
        //{
        //    var accountRepositoryMock = new Mock<AccountRepository>();

        //    //GetPermissions +virtual
        //    accountRepositoryMock.Setup(x => x.GetPermissions(It.IsAny<long>())).Returns(Permissions);
        //    var menus = accountRepositoryMock.Object.GetMenus(1);
        //    Assert.IsNotNull(menus);
        //    Assert.AreEqual(2, menus.Count);
        //    Assert.AreEqual(
        //        string.Join(",", Menus.Select(x => x.Id)),
        //        string.Join(",", menus.Select(x => x.Id)));

        //    accountRepositoryMock.Verify(x => x.GetPermissions(It.IsAny<long>()), Times.Once);
        //}

        [TestMethod]
        public void TestGetMenus()
        {
            var menus = AccountRepository.GetMenus(1);
            Assert.AreEqual(3, menus.Count);
        }

        [TestMethod]
        public void TestVerifyLogin()
        {
            var account = AccountRepository.VerifyLogin(new LoginUser { UserName = "Admin", Password = "123456" });
            Assert.IsNotNull(account);
        }

        [TestMethod]
        public void TestGetById()
        {
            var account = AccountRepository.GetById(1);
            Assert.IsNotNull(account);
        }

        [TestMethod]
        public void TestSoftDelete()
        {
            AccountRepository.Delete(AccountRepository.Entities);
            Assert.IsTrue(AccountRepository.Entities.Any());
            foreach (var account in AccountRepository.Entities)
            {
                Assert.AreEqual(true, account.IsDeleted);
            }
        }

        [TestMethod]
        public void TestUpdateRegister()
        {
            var account = AccountRepository[1];
            account.Register.LastLoginIP = null;
            AccountRepository.Update(account);
        }
    }
}
