using System.Data.Entity;
using System.Linq;
using EasyAccess.Repository.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyAccess.UnitTest.TestRepository
{
    [TestClass]
    public class EasyAccessInitializerTest
    {
        public static EasyAccessContext EasyAccessCtx = new EasyAccessContext();

        [AssemblyInitialize()]
        public static void AssemblyInit(TestContext context)
        {
            Database.SetInitializer<EasyAccessContext>(new EasyAccessInitializer());
        }

        [TestMethod]
        public void TestSeed()
        {
            Assert.IsTrue(1 == EasyAccessCtx.Accounts.Count());
        }
    }
}
