using System;
using System.Data.Entity;
using EasyAccess.Infrastructure.Repository;

namespace EasyAccess.Infrastructure.UnitOfWork
{
    public class UnitOfWorkBase : IUnitOfWork
    {
        private readonly DbContext _dbContexts;

        public UnitOfWorkBase(DbContext dbContext)
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
