using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyAccess.UnitTest.TestInfrastructure.TestRepository
{
    [TestClass]
    public class RepositoryBaseTest
    {
        [TestMethod]
        public void TestLocal_Expression()
        {
            Assert.AreEqual("Id", GetClassPropertyName<Person>(x => "hello"));
        }

        public string GetClassPropertyName<T>(Expression<Func<T, object>> expr)
        {
            var rtn = "";
            if (expr.Body is UnaryExpression)
            {
                rtn = ((MemberExpression) ((UnaryExpression) expr.Body).Operand).Member.Name;
            }
            else if (expr.Body is MemberExpression)
            {
                rtn = ((MemberExpression) expr.Body).Member.Name;
            }
            else if(expr.Body is ParameterExpression)
            {
                rtn = ((ParameterExpression) expr.Body).Type.Name;
            }
            return rtn;
        }

        public class Person
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public MyClass Class { get; set; }

            public string GetName()
            {
                return this.Name;
            }
        }

        public class MyClass
        {
             public Guid Id { get; set; }
        }
    }
}
