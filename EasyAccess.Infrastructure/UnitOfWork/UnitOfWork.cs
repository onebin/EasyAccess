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
        private List<DbContext> _dbContexts;

        public void GetRepostory<TEntity, TRepositity>(TRepositity repository)
            where TEntity : class
            where TRepositity : RepositoryBase<TEntity>
        {
            throw new NotImplementedException();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dbContexts.ForEach(x => x.Dispose());
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
