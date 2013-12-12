using Demo.Repository.Repositories;
using EasyAccess.Infrastructure.UnitOfWork;
using EasyAccess.Repository.Repositories;
using EasyAccess.Service.IServices;
using Spring.Context.Support;
using Spring.Testing.Microsoft;

namespace Demo.MvcApplication.Tests.Bootstrap
{
    public abstract class SpringTestBase : AbstractDependencyInjectionSpringContextTests
    {
        protected override string[] ConfigLocations
        {
            get
            {
                return new string[]
                {
                    "assembly://EasyAccess.Repository/EasyAccess.Repository/Bootstrap.Spring.RepositoryConfig.generated.xml",
                    "assembly://EasyAccess.Service/EasyAccess.Service/Bootstrap.Spring.ServiceConfig.xml",
                    "assembly://Demo.Repository/Demo.Repository/Bootstrap.Spring.RepositoryConfig.generated.xml",
                    "assembly://Demo.Service/Demo.Service/Bootstrap.Spring.ServiceConfig.xml",
                    "assembly://Demo.MvcApplication/Demo.MvcApplication/ControllerConfig.xml"
                };
            }
        }

        protected IUnitOfWork EasyAccessUnitOfWork { get; private set; }

        protected IUnitOfWork DemoUnitOfWork { get; private set; }

        protected AccountRepository AccountRepository { get; private set; }

        protected RoleRepository RoleRepository { get; private set; }

        protected IAccountManageSvc AccountManageService  { get; private set; }

        protected ArticleConfigRepository ArticleConfigRepository { get; set; }

        protected SectionConfigRepository SectionConfigRepository { get; set; }


        protected SpringTestBase()
        {
            var appCtx = ContextRegistry.GetContext();
            EasyAccessUnitOfWork = appCtx.GetObject("EasyAccessUnitOfWork") as IUnitOfWork;
            DemoUnitOfWork = appCtx.GetObject("DemoUnitOfWork") as IUnitOfWork;
            AccountRepository = appCtx.GetObject<AccountRepository>();
            RoleRepository = appCtx.GetObject<RoleRepository>();
            AccountManageService = appCtx.GetObject<IAccountManageSvc>();

            ArticleConfigRepository = appCtx.GetObject<ArticleConfigRepository>();
            SectionConfigRepository = appCtx.GetObject<SectionConfigRepository>();
        }
    }
}