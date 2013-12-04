using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
using EasyAccess.Infrastructure.Entity;
using EasyAccess.Infrastructure.Repository;
using EasyAccess.Infrastructure.UnitOfWork;
using EasyAccess.Infrastructure.Util.ConditionBuilder;
using EasyAccess.Infrastructure.Util.PagingData;

namespace EasyAccess.Infrastructure.Service
{
    public abstract class ServiceBase
    {
        protected IUnitOfWork UnitOfWork { get; set; }
    }
}
