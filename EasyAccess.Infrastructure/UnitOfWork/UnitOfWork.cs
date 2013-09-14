using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using EasyAccess.Infrastructure.RepositoryFramework;

namespace EasyAccess.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContexts;

        public UnitOfWork(DbContext dbContext)
        {
            _dbContexts = dbContext;
        }

        public TRepositity GetRepostory<TRepositity>() where TRepositity : IRepository
        {
            return (TRepositity)Activator.CreateInstance(typeof (TRepositity), _dbContexts);
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dbContexts.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
