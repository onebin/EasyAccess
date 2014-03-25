using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using EasyAccess.Infrastructure.Entity;
using EasyAccess.Infrastructure.Util.T4;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyAccess.UnitTest.TestInfrastructure.TestUtil
{
    [TestClass]
    public class T4TempDllTest
    {
        [TestMethod]
        public void TestT4TempDll()
        {
            var tempT4Dll = new T4TempDll(@"D:\Workspace\GitHub\EasyAccess\", "EasyAccess.Model");

            Assert.IsTrue(new Regex("EasyAccess.Model" + "\\.\\d{8}\\.dll$").IsMatch(tempT4Dll.TempDllFullName));
        }

        [TestMethod]
        public void TestLoadFrom()
        {
            var tempT4Dll = new T4TempDll(@"D:\Workspace\GitHub\EasyAccess\", "EasyAccess.Model");
            var assembly = Assembly.LoadFrom(tempT4Dll.TempDllFullName);
            if (assembly != null)
            {
                var entities = assembly.GetTypes().Where(x => typeof(IEntity).IsAssignableFrom(x) && !x.IsAbstract).ToList(); 
                Assert.IsTrue(entities.Count > 0);
            }
        }
    }
}
