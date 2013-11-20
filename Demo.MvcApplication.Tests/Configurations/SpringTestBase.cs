using Demo.Repository.IRepositories;
using EasyAccess.Infrastructure.UnitOfWork;
using EasyAccess.Repository.IRepositories;
using EasyAccess.Service.IServices;
using Spring.Context.Support;
using Spring.Testing.Microsoft;

namespace Demo.MvcApplication.Tests.Configurations
{
    public abstract class SpringTestBase : AbstractDependencyInjectionSpringContextTests
    {
        protected override string[] ConfigLocations
        {
            get
            {
                return new string[]
                {
                    "assembly://EasyAccess.Repository/EasyAccess.Repository/Configurations.Spring.RepositoryConfig.xml",
                    "assembly://EasyAccess.Service/EasyAccess.Service/Configurations.Spring.ServiceConfig.xml",
                    "assembly://Demo.Repository/Demo.Repository/Configurations.Spring.RepositoryConfig.xml",
                    "assembly://Demo.Service/Demo.Service/Configurations.Spring.ServiceConfig.xml",
                    "assembly://Demo.MvcApplication/Demo.MvcApplication/ControllerConfig.xml"
                };
            }
        }

        protected IUnitOfWork EasyAccessUnitOfWork { get; private set; }

        protected IUnitOfWork DemoUnitOfWork { get; private set; }

        protected IAccountRepository AccountRepository { get; private set; }

        protected IRoleRepository RoleRepository { get; private set; }

        protected IAccountManageSvc AccountManageService  { get; private set; }

        protected IArticleConfigRepository ArticleConfigRepository { get; set; }

        protected ISectionConfigRepository SectionConfigRepository { get; set; }

        protected IInputConfigRepository InputConfigRepository { get; set; }

        protected SpringTestBase()
        {
            var appCtx = ContextRegistry.GetContext();
            EasyAccessUnitOfWork = appCtx.GetObject("EasyAccessUnitOfWork") as IUnitOfWork;
            DemoUnitOfWork = appCtx.GetObject("DemoUnitOfWork") as IUnitOfWork;
            AccountRepository = appCtx.GetObject<IAccountRepository>();
            RoleRepository = appCtx.GetObject<IRoleRepository>();
            AccountManageService = appCtx.GetObject<IAccountManageSvc>();

            ArticleConfigRepository = appCtx.GetObject<IArticleConfigRepository>();
            SectionConfigRepository = appCtx.GetObject<ISectionConfigRepository>();
            InputConfigRepository = appCtx.GetObject<IInputConfigRepository>();
        }
    }
}