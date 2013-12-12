using System;
using System.Linq;
using Demo.Model.EDMs;
using Demo.MvcApplication.Tests.Bootstrap;
using EasyAccess.Model.EDMs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Demo.MvcApplication.Tests.TestRepository
{
    [TestClass]
    public class ArticleConfigRepositoryTest: SpringTestBase
    {
        [TestMethod]
        public void TestGetById()
        {
            var article = ArticleConfig.FindById(1);
            Assert.IsNotNull(article);
        }
    }
}
