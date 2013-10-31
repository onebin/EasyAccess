using System;
using System.Linq;
using Demo.MvcApplication.Tests.Configurations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Demo.MvcApplication.Tests.TestRepository
{
    [TestClass]
    public class ArticleConfigRepositoryTest: SpringTestBase
    {
        [TestMethod]
        public void TestBatchDelete()
        {
            SectionConfigRepository.Delete(SectionConfigRepository.Entities);
            Assert.IsFalse(SectionConfigRepository.Entities.Any());
        }
    }
}
