using System;
using EasyAccess.Infrastructure.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyAccess.UnitTest.TestInfrastructure.TestExtensions
{
    [TestClass]
    public class TestTypeExtension
    {
        enum MyEnum
        {
            A = 0,
            B = 1
        }

        class MyClass
        {
            public int NonNullableInt { get; set; }
            public decimal NonNullableDecimal { get; set; }
            public float NonNullableFloat { get; set; }
            public double NonNullableDouble { get; set; }
            public byte NonNullableByte { get; set; }
            public string NonNullableString { get; set; }
            public DateTime NonNullableDateTime { get; set; }
            public MyEnum NonNullableMyEnum { get; set; }
            public MyInnerClass NonNullableInnerClass { get; set; }

            public int? NullableInt { get; set; }
            public decimal? NullableDecimal { get; set; }
            public float? NullableFloat { get; set; }
            public double? NullableDouble { get; set; }
            public byte? NullableByte { get; set; }
            public MyEnum? NullableMyEnum { get; set; }
            public DateTime? NullableDateTime { get; set; }
        }

        class MyInnerClass
        {
        }

        [TestMethod]
        public void TestIsNullableType()
        {
            var myClass = typeof (MyClass);
            Assert.IsTrue(myClass.GetProperty("NullableInt").PropertyType.IsNullableType());
            Assert.IsTrue(myClass.GetProperty("NullableDecimal").PropertyType.IsNullableType());
            Assert.IsTrue(myClass.GetProperty("NullableFloat").PropertyType.IsNullableType());
            Assert.IsTrue(myClass.GetProperty("NullableDouble").PropertyType.IsNullableType());
            Assert.IsTrue(myClass.GetProperty("NullableByte").PropertyType.IsNullableType());
            Assert.IsTrue(myClass.GetProperty("NullableMyEnum").PropertyType.IsNullableType());
            Assert.IsTrue(myClass.GetProperty("NullableDateTime").PropertyType.IsNullableType());

            Assert.IsFalse(myClass.GetProperty("NonNullableInt").PropertyType.IsNullableType());
            Assert.IsFalse(myClass.GetProperty("NonNullableDecimal").PropertyType.IsNullableType());
            Assert.IsFalse(myClass.GetProperty("NonNullableFloat").PropertyType.IsNullableType());
            Assert.IsFalse(myClass.GetProperty("NonNullableDouble").PropertyType.IsNullableType());
            Assert.IsFalse(myClass.GetProperty("NonNullableByte").PropertyType.IsNullableType());
            Assert.IsFalse(myClass.GetProperty("NonNullableString").PropertyType.IsNullableType());
            Assert.IsFalse(myClass.GetProperty("NonNullableMyEnum").PropertyType.IsNullableType());
            Assert.IsFalse(myClass.GetProperty("NonNullableInnerClass").PropertyType.IsNullableType());
        }

        [TestMethod]
        public void TestIsBaseDataType()
        {
            var myClass = typeof(MyClass);
            Assert.IsTrue(myClass.GetProperty("NullableInt").PropertyType.IsBasic());
            Assert.IsTrue(myClass.GetProperty("NullableDecimal").PropertyType.IsBasic());
            Assert.IsTrue(myClass.GetProperty("NullableFloat").PropertyType.IsBasic());
            Assert.IsTrue(myClass.GetProperty("NullableDouble").PropertyType.IsBasic());
            Assert.IsTrue(myClass.GetProperty("NullableByte").PropertyType.IsBasic());
            Assert.IsTrue(myClass.GetProperty("NullableMyEnum").PropertyType.IsBasic());
            Assert.IsTrue(myClass.GetProperty("NullableDateTime").PropertyType.IsBasic());

            Assert.IsTrue(myClass.GetProperty("NonNullableInt").PropertyType.IsBasic());
            Assert.IsTrue(myClass.GetProperty("NonNullableDecimal").PropertyType.IsBasic());
            Assert.IsTrue(myClass.GetProperty("NonNullableFloat").PropertyType.IsBasic());
            Assert.IsTrue(myClass.GetProperty("NonNullableDouble").PropertyType.IsBasic());
            Assert.IsTrue(myClass.GetProperty("NonNullableByte").PropertyType.IsBasic());
            Assert.IsTrue(myClass.GetProperty("NonNullableString").PropertyType.IsBasic());
            Assert.IsTrue(myClass.GetProperty("NonNullableMyEnum").PropertyType.IsBasic());
            
            Assert.IsFalse(myClass.GetProperty("NonNullableInnerClass").PropertyType.IsBasic());
        }

        [TestMethod]
        public void TestIsNullableOf()
        {
            var myClass = typeof(MyClass);
            Assert.IsTrue(myClass.GetProperty("NullableInt").PropertyType.IsNullableOf(typeof(int)));
            Assert.IsTrue(myClass.GetProperty("NullableDecimal").PropertyType.IsNullableOf(typeof(decimal)));
            Assert.IsTrue(myClass.GetProperty("NullableFloat").PropertyType.IsNullableOf(typeof(float)));
            Assert.IsTrue(myClass.GetProperty("NullableDouble").PropertyType.IsNullableOf(typeof(double)));
            Assert.IsTrue(myClass.GetProperty("NullableByte").PropertyType.IsNullableOf(typeof(byte)));
            Assert.IsTrue(myClass.GetProperty("NullableMyEnum").PropertyType.IsNullableOf(typeof(MyEnum)));
            Assert.IsTrue(myClass.GetProperty("NullableDateTime").PropertyType.IsNullableOf(typeof(DateTime)));
        }
    }
}
