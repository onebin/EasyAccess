using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyAccess.Infrastructure.Util.PagingData
{
    /// <summary>
    ///  数据分页数据类
    /// </summary>
    /// <typeparam name="TEntity">数据实体类型</typeparam>
    public class PagingData<TEntity> where TEntity:class
    {
        /// <summary>
        ///  记录总数
        /// </summary>
        public long RecordCount { set; get; }

        /// <summary>
        /// 页数
        /// </summary>
        public long PageCount 
        {
            get { return (long)Math.Ceiling(RecordCount / (double)PagingConditon.PageSize); } 
        }

        public PagingCondition PagingConditon { get; set; }

        /// <summary>
        ///  数据
        /// </summary>
        public List<TEntity> RecordData { set; get; }

        /// <summary>
        ///  默认构造方法
        /// </summary>
        public PagingData() { 
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="recordCount">记录数</param>
        /// <param name="pagingConditon">分页条件</param>
        /// <param name="recordData">数据</param>
        public PagingData(long recordCount, PagingCondition pagingConditon, IEnumerable<TEntity> recordData)
        {
            this.RecordCount = recordCount;
            this.PagingConditon = pagingConditon;
            this.RecordData = recordData.ToList();
        }
    }
}
