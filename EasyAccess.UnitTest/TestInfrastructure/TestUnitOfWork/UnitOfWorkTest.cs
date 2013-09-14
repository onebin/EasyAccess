using System.Dynamic;
using System.Linq;
using EasyAccess.Infrastructure.Repositories;
using EasyAccess.Infrastructure.UnitOfWork;
using EasyAccess.Infrastructure.UnitOfWorkFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spring.Context;
using Spring.Context.Support;
using Spring.Testing.Microsoft;

namespace EasyAccess.UnitTest.TestInfrastructure.TestUnitOfWork
{
    [TestClass]
    public class UnitOfWorkTest : AbstractDependencyInjectionSpringContextTests
    {
        private IUnitOfWork EasyAccessUnitOfWork { get; set; }
        private static IApplicationContext AppCtx { get; set; }

        protected override string[] ConfigLocations
        {
            get
            {
                return new string[]
                {
                    "assembly://EasyAccess.UnitTest/EasyAccess.UnitTest/SpringConfig.ContextConfig.xml",
                    "assembly://EasyAccess.UnitTest/EasyAccess.UnitTest/SpringConfig.IocConfig.xml"
                };
            }
        }

        [ClassInitialize]
        public static void InitTestSuite(TestContext testContext)
        {
            AppCtx = ContextRegistry.GetContext();
        }

        [TestInitialize()]
        public void InitTest()
        {
            EasyAccessUnitOfWork = AppCtx.GetObject("EasyAccessUnitOfWork") as IUnitOfWork;
        }

        [TestMethod]
        public void TestIoc()
        {
            Assert.IsNotNull(EasyAccessUnitOfWork);
        }

        [TestMethod]
        public void TestGetRepostoryByNew()
        {
            var unitOfWork = new UnitOfWork(new EasyAccessContext());
            var accountRepository = unitOfWork.GetRepostory<AccountRepository>();
            Assert.AreEqual("Ebin", accountRepository.GetAll().First().FirstName);
        }

        [TestMethod]
        public void TestGetRepostoryByIoc()
        {
            var accountRepository = EasyAccessUnitOfWork.GetRepostory<AccountRepository>();
            Assert.AreEqual("Ebin", accountRepository.GetAll().First().FirstName);
        }
    }
}
