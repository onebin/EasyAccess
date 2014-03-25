using System;
using System.Text.RegularExpressions;
using EasyAccess.Infrastructure.Util.T4;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyAccess.UnitTest.TestInfrastructure.TestUtil
{
    [TestClass]
    public class T4TempDllTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(new Regex("abc" + "\\.\\d{8}\\.dll$").IsMatch("abc.03251111.dll"));
        }

        [TestMethod]
        public void TestT4TempDll()
        {
            var tempT4Dll = new T4TempDll(@"D:\Workspace\GitHub\EasyAccess\", "EasyAccess.Infrastructure");

            Assert.IsTrue(new Regex("EasyAccess.Infrastructure" + "\\.\\d{8}\\.dll$").IsMatch(tempT4Dll.TempDllFullName));
        }
    }
}
