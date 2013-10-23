using System;
using System.Data.Entity;
using EasyAccess.Infrastructure.UnitOfWork;
using EasyAccess.Repository.Configurations;
using EasyAccess.Repository.Configurations.EntityFramework;

namespace EasyAccess.Repository.UnitOfWork
{
    public class EasyAccessUnitOfWork : UnitOfWorkContextBase
    {
        public EasyAccessUnitOfWork(EasyAccessContext context)
        {
            EasyAccessContext = context;
        }

        protected EasyAccessContext EasyAccessContext { get; private set; }

        protected override DbContext DbContext
        {
            get { return EasyAccessContext; }
        }
    }
}
