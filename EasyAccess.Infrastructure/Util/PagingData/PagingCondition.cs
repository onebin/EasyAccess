using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyAccess.Infrastructure.Util.PagingData
{
    public class PagingCondition
    {
        public PagingCondition() { }

        public PagingCondition(int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
        }

        /// <summary>
        /// 页索引
        /// </summary>
        public int PageIndex { set; get; }

        /// <summary>
        ///  页面大小
        /// </summary>
        public int PageSize { set; get; }

        /// <summary>
        /// 略过数量
        /// </summary>
        public int Skip
        {
            get
            {
                return PageIndex * PageSize;
            }
        }
    }
}
