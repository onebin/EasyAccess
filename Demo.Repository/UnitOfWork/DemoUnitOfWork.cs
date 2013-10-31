using System.Data.Entity;
using Demo.Repository.Configurations.EntityFramework;
using EasyAccess.Infrastructure.UnitOfWork;

namespace Demo.Repository.UnitOfWork
{
    public class DemoUnitOfWork: UnitOfWorkContextBase
    {
        public DemoUnitOfWork(DemoContext context)
        {
            DemoContext = context;
        }

        protected DemoContext DemoContext { get; private set; }

        protected override DbContext DbContext
        {
            get { return DemoContext; }
        }
    }
}