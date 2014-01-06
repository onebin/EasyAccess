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
        /// 当前工作单元是否回滚
        /// </summary>
        bool IsRollback { get; }

        /// <summary>
        /// 提交单元操作
        /// </summary>
        /// <param name="force">是否强制提交（无论当工作单元是否已提交，都强制执行）</param>
        /// <returns>受影响行数</returns>
        int Commit(bool force = false);

        /// <summary>
        /// 把工作单元回滚至未提交状态
        /// </summary>
        void Rollback();
    }
}
