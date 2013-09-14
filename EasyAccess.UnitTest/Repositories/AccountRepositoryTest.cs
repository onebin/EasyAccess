using System;
using System.Data.Entity;
using System.Linq;
using EasyAccess.Infrastructure.Repositories;
using EasyAccess.Infrastructure.UnitOfWork;
using EasyAccess.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyAccess.UnitTest.Repositories
{
    [TestClass]
    public class AccountRepositoryTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var unitOfWork = new UnitOfWork(new EasyAccessContext());
            var accountRepository = unitOfWork.GetRepostory<AccountRepository>();
            Assert.AreEqual(1, accountRepository.GetAll().Count());
            Assert.AreEqual("Ebin", accountRepository.GetAll().First().FirstName);
        }
    }
}
