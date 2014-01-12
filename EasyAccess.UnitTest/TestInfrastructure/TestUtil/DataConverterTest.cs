using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using EasyAccess.Infrastructure.Util.DataConverter;
using EasyAccess.Model.EDMs;
using EasyAccess.Model.VOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyAccess.UnitTest.TestInfrastructure.TestUtil
{
    [TestClass]
    public class DataConverterTest
    {
        private static DataTable defaultTable;
        private static DataTable defaultTableMissColumns;
        private static DataTable customTable;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            customTable = new DataTable("用户");
            defaultTable = new DataTable("Account");
            defaultTableMissColumns = new DataTable("Account");

            customTable.Columns.AddRange(new[]
            {
                new DataColumn("年龄"),
                new DataColumn("性别"),
                new DataColumn("备注")
            });
            defaultTable.Columns.AddRange(new[]
            {
                new DataColumn("Id"),
                new DataColumn("Age"),
                new DataColumn("Sex"),
                new DataColumn("Memo"),
                new DataColumn("IsDeleted"),
                new DataColumn("CreateTime")
            });
            defaultTableMissColumns.Columns.AddRange(new[]
            {
                new DataColumn("Id"),
                new DataColumn("Age"),
                new DataColumn("Sex"),
                new DataColumn("Memo"),
                new DataColumn("IsDeleted")
            });

            var customRow1 = customTable.NewRow();
            customRow1["年龄"] = 21;
            customRow1["性别"] = "Male";
            customRow1["备注"] = "帅哥";
            var customRow2= customTable.NewRow();
            customRow2["年龄"] = 20;
            customRow2["性别"] = "Female";
            customRow2["备注"] = "美女";
            customTable.Rows.Add(customRow1);
            customTable.Rows.Add(customRow2);
        }

        [TestMethod]
        public void TestConvertToListOptions()
        {
            var options = new ConvertToListOptions();
            options.MapTable(typeof(Account), "用户");
            options.MapColumn<Account>(x => x.Age, "年龄");

            Assert.IsTrue(options.TableMapper.ContainsKey("用户"));
            Assert.IsTrue(options.TableMapper.ContainsValue("Account"));

            Assert.IsTrue(options.ColumnMapper.ContainsKey("Account"));
            Assert.IsTrue(options.ColumnMapper["Account"].ContainsKey("年龄"));
            Assert.AreEqual("Age", options.ColumnMapper["Account"]["年龄"].Name);
            Assert.AreEqual(typeof(int), options.ColumnMapper["Account"]["年龄"].PropertyType);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TestDefaultTableMissRequireColumn()
        {
            var convert = new DataConverter<Account>();
            convert.ToList(defaultTableMissColumns);
            Assert.Fail("期望抛出KeyNotFoundException，而实际没有抛出");
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TestCustomTableMissRequireColumn()
        {
            var options = new ConvertToListOptions();
            options.MapTable(typeof(Account), "用户");
            options.MapColumn<Account>(x => x.IsDeleted, "是否删除");
            var convert = new DataConverter<Account>();
            convert.ToList(customTable, options);
            Assert.Fail("期望抛出KeyNotFoundException，而实际没有抛出");
        }

        [TestMethod]
        public void TestCustomTable()
        {
            var options = new ConvertToListOptions();
            options.MapTable(typeof(Account), "用户");
            options.MapColumn<Account>(x => x.Age, "年龄");
            options.MapColumn<Account>(x => x.Sex, "性别");
            options.MapColumn<Account>(x => x.Memo, "备注");
            var convert = new DataConverter<Account>();
            var lst = convert.ToList(customTable, options);
            Assert.AreEqual(2, lst.Count);
            Assert.AreEqual(21, lst.First().Age);
            Assert.AreEqual(Sex.Male, lst.First().Sex);
            Assert.AreEqual("帅哥", lst.First().Memo);
            Assert.AreEqual(20, lst.Last().Age);
            Assert.AreEqual(Sex.Female, lst.Last().Sex);
            Assert.AreEqual("美女", lst.Last().Memo);
        }
    }
}
