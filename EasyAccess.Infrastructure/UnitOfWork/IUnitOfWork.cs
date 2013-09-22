using System;
using EasyAccess.Infrastructure.Repository;

namespace EasyAccess.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        TRepositity GetRepostory<TRepositity>()
            where TRepositity : IRepository;

        int Commit();
    }
}
