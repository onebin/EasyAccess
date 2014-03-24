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
            var enumDescriptions = EnumDescriptionManager.GetEnumDescriptions(typeof (Color));
            Assert.AreEqual(4, enumDescriptions.Count());
            Assert.AreEqual("红色", enumDescriptions[0].Description);
            Assert.AreEqual("蓝色", enumDescriptions[1].Description);
            Assert.AreEqual("绿色", enumDescriptions[2].Description);
            Assert.AreEqual("黄色", enumDescriptions[3].Description);

            Assert.AreEqual(10, enumDescriptions[0].Value);
            Assert.AreEqual(20, enumDescriptions[1].Value);
            Assert.AreEqual(30, enumDescriptions[2].Value);
            Assert.AreEqual(40, enumDescriptions[3].Value);

            var enumBlue = EnumDescriptionManager.GetDescription(Color.Blue);
            Assert.AreEqual("蓝色", enumBlue);
        }
    }
}
