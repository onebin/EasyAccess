using System;
using EasyAccess.Infrastructure.Repository;

namespace EasyAccess.Infrastructure.UnitOfWorkFramework
{
    public interface IUnitOfWork : IDisposable
    {
        TRepositity GetRepostory<TRepositity>()
            where TRepositity : IRepository;
    }
}
