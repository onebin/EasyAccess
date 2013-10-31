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
            ArticleConfigRepository.Delete(ArticleConfigRepository.Entities.Where(x => x.Id == 1).Select(x => x.Sections));
            Assert.IsFalse(ArticleConfigRepository.Entities.Any());
        }
    }
}
