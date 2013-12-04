using System;
using System.Linq;
using Demo.Model.EDMs;
using Demo.MvcApplication.Tests.Bootstrap;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Demo.MvcApplication.Tests.TestRepository
{
    [TestClass]
    public class ArticleConfigRepositoryTest: SpringTestBase
    {
        [TestMethod]
        public void TestBatchDelete()
        {
            SectionConfig.Repository.Delete(SectionConfigRepository.Entities);
            Assert.IsFalse(SectionConfig.Repository.Entities.Any());
        }
    }
}
