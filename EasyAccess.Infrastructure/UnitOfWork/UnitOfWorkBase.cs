using System;
using System.Data.Entity;
using EasyAccess.Infrastructure.Repository;

namespace EasyAccess.Infrastructure.UnitOfWork
{
    public class UnitOfWorkBase : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public UnitOfWorkBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TRepositity GetRepostory<TRepositity>() where TRepositity : IRepository
        {
            return (TRepositity)Activator.CreateInstance(typeof (TRepositity), _dbContext);
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }
    }
}
