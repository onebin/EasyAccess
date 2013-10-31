using Demo.Repository.IRepositories;
using Demo.Service.IServices;
using EasyAccess.Infrastructure.Service;

namespace Demo.Service.Services
{
    public class SubjectManageService : ServiceBase, ISubjectManageService
    {
        public IArticleConfigRepository ArticleConfigRepository { get; set; }

        public ISectionConfigRepository SectionConfigRepository { get; set; }

        public IInputConfigRepository InputConfigRepository { get; set; }
    }
}