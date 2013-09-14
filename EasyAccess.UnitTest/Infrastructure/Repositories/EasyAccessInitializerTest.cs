using System;
using System.Data.Entity;
using System.Linq;
using EasyAccess.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyAccess.UnitTest.Infrastructure.Repositories
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
