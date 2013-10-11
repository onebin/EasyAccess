using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Model;
using EasyAccess.Infrastructure.Repository;

namespace Demo.Repository.IRepositories
{
    public interface IProviderRepository : IRepositoryBase<Provider, long>
    {
    }
}
