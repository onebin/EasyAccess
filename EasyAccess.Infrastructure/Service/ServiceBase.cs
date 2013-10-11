using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyAccess.Infrastructure.UnitOfWork;

namespace EasyAccess.Infrastructure.Service
{
    public abstract class ServiceBase
    {
        protected IUnitOfWork UnitOfWork { get; set; }
    }
}
