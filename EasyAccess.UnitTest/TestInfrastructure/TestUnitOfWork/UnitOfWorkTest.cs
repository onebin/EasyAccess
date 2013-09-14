using System.Linq;
using EasyAccess.Infrastructure.Repositories;
using EasyAccess.Infrastructure.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyAccess.UnitTest.TestInfrastructure.TestUnitOfWork
{
    [TestClass]
    public class UnitOfWorkTest
    {
        [TestMethod]
        public void TestGetRepostory()
        {
            var unitOfWork = new UnitOfWork(new EasyAccessContext());
            var accountRepository = unitOfWork.GetRepostory<AccountRepository>();
            Assert.AreEqual("Ebin", accountRepository.GetAll().First().FirstName);
        }
    }
}
