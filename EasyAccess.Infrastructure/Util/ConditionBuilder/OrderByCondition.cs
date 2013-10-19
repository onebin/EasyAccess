using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyAccess.Infrastructure.Util.ConditionBuilder
{
    public class OrderByCondition
    {
        public Expression KeySelector { get; set; }

        public ListSortDirection Direction { get; set; }
    }
}
