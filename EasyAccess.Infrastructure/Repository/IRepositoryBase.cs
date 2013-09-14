using System.Collections.Generic;

namespace EasyAccess.Infrastructure.Repository
{
    interface IRepositoryBase<TEntity> : IRepository where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(params object[] id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(params object[] id);
    }
}
