using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyAccess.Infrastructure.RepositoryFramework;

namespace EasyAccess.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void GetRepostory<TEntity, TRepositity>(TRepositity repository) 
            where TEntity : class 
            where TRepositity : RepositoryBase<TEntity>;
    }
}
