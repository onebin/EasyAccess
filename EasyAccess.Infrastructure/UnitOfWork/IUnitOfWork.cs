using System;
using EasyAccess.Infrastructure.Repository;

namespace EasyAccess.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        /// <summary>
        /// 当前工作单元是否已提交
        /// </summary>
        bool IsCommitted { get; }
        
        /// <summary>
        /// 提交单元操作
        /// </summary>
        /// <returns>受影响行数</returns>
        int Commit();

        /// <summary>
        /// 把工作单元回滚至未提交状态
        /// </summary>
        void Rollback();
    }
}
