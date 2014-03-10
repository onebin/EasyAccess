using System;
using System.Linq;
using System.Runtime.InteropServices;
using EasyAccess.Infrastructure.Util.EnumDescription;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyAccess.UnitTest.TestInfrastructure.TestEnumDescription
{
    [TestClass]
    public class EnumDescriptionTest
    {
        internal enum Color
        {
            [EnumDescription("红色", 2)]
            Red = 10,

            [EnumDescription("蓝色", 4)]
            Blue = 20,

            [EnumDescription("绿色", 3)]
            Green = 30,

            [EnumDescription("黄色", 1)]
            Yellow = 40,
        }

        [TestMethod]
        public void TestGetEnumDescriptions()
        {
            var enumDescriptions = EnumDescriptionManager.Instance.GetEnumDescriptions(typeof (Color), EnumSortType.Index);
            Assert.AreEqual(4, enumDescriptions.Count());

            var enumBlue = EnumDescriptionManager.Instance.GetDescription(Color.Blue);
            Assert.AreEqual("蓝色", enumBlue);
        }
    }
}
