using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EasyAccess.Infrastructure.Repository;
using EasyAccess.Model.EDMs;
using EasyAccess.Model.VOs;
using EasyAccess.UnitTest.Bootstrap;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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

        private static readonly ICollection<Account> Accounts = new Collection<Account>
            {
                new Account() {Id = 1, Roles = Roles}
            };


        public class AccountAdapter: Account
        {
            public new static IRepositoryBase<Account> Repository
            {
                get
                {
                    var repositoryMock = new Mock<IRepositoryBase<Account>>();
                    repositoryMock.SetupProperty(x => x.Entities, new EnumerableQuery<Account>(Accounts));
                    return repositoryMock.Object;
                }
            }
        }

        [TestMethod]
        public void TestGetPermissions()
        {
            var accountMock = new Mock<AccountAdapter>();
            accountMock.SetupProperty(x => x.Roles, Roles);
            //GetPermissions -virtual ， Roles +virtual
            var permissions = accountMock.Object.GetPermissions();
            Assert.IsNotNull(permissions);
            Assert.AreEqual(5, permissions.Count);
            Assert.AreEqual(
                string.Join(",", Permissions.Where(x => x.Id != "M01P0104").Select(x => x.Id)),
                string.Join(",", permissions.Select(x => x.Id)));
        }

        //[TestMethod]
        //public void TestGetPermissions()
        //{
        //    var permissions = Account.Repository[1].GetPermissions();
        //    Assert.AreEqual(9, permissions.Count);
        //}

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
            var menus = Account.FindById(1).GetMenus();
            Assert.AreEqual(3, menus.Count);
        }

        [TestMethod]
        public void TestVerifyLogin()
        {
            var account = Account.VerifyLogin(new LoginUser { UserName = "Admin", Password = "123456" });
            Assert.IsNotNull(account);
        }

        [TestMethod]
        public void TestGetById()
        {
            var account1 = Account.FindById(1);
            Assert.IsNotNull(account1);
            var account2 = Account.FindById(2);
        }

        [TestMethod]
        public void TestSoftDelete()
        {
            //Account.Delete(Account.FindAll());
            var accounts = Account.FindAll();
            Assert.IsTrue(accounts.Any());
            foreach (var account in Account.FindAll())
            {
                Assert.AreEqual(false, account.IsDeleted);
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
