using Demo.Model;
using Demo.Repository.IRepositories;
using EasyAccess.Infrastructure.Repository;

namespace Demo.Repository.Repositories
{
    public class ProviderRepository : RepositoryBase<Provider, long>, IProviderRepository
    {

    }
}
