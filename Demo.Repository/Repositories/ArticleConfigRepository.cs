using Demo.Model.EDMs;
using Demo.Repository.IRepositories;
using EasyAccess.Infrastructure.Repository;

namespace Demo.Repository.Repositories
{
    public class ArticleConfigRepository : RepositoryBase<ArticleConfig>, IArticleConfigRepository
    {
         
    }
}