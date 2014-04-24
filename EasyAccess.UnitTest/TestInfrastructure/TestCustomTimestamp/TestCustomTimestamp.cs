using System;
using EasyAccess.Model.EDMs;
using EasyAccess.Model.VOs;
using EasyAccess.UnitTest.Bootstrap;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyAccess.UnitTest.TestInfrastructure.TestCustomTimestamp
{
    [TestClass]
    public class TestCustomTimestamp: SpringTestBase
    {
        [TestMethod]
        public void TestUpdateMode_GreaterThan()
        {
            var test = Test.FindById(1);
            test.NonNullableInt = 100;
            test.NonNullableDecimal = 100;
            test.NonNullableFloat = 100;
            test.NonNullableDouble = 100;
            test.NonNullableByte = 100;
            test.NonNullableString = "~!@#$%^&*()_+={}[]|\\?.,<>--！@#￥%……&*（）——《》？:\"''/*jhgfj*/\\*564\\*";
            test.NonNullableDateTime = DateTime.UtcNow;
            test.NonNullableSexEnum = Sex.Unknown;

            test.NullableInt = null;
            test.NullableDecimal = null;
            test.NullableFloat = null;
            test.NullableDouble = null;
            test.NullableByte = null;
            test.NullableDateTime = null;
            test.NullableSexEnum = null;
            test.RowVersion = 8;
            Assert.AreEqual(1, EasyAccessUnitOfWork.Commit());

        }
    }
}
