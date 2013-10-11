using System;
using System.Data.Entity;
using EasyAccess.Infrastructure.UnitOfWork;
using EasyAccess.Repository.Configuration;

namespace EasyAccess.Repository.UnitOfWork
{
    public class EasyAccessUnitOfWork : UnitOfWorkContextBase
    {
        public EasyAccessContext EasyAccessContext { get; set; }

        protected override DbContext DbContext
        {
            get { return EasyAccessContext; }
        }
    }
}
