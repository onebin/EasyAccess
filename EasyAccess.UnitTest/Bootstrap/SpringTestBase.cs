using EasyAccess.Infrastructure.UnitOfWork;
using EasyAccess.Repository.Bootstrap.EntityFramework;
using EasyAccess.Repository.Repositories;
using EasyAccess.Service.IServices;
using Spring.Context.Support;
using Spring.Testing.Microsoft;

namespace EasyAccess.UnitTest.Bootstrap
{
    public abstract class SpringTestBase : AbstractDependencyInjectionSpringContextTests
    {
        static SpringTestBase()
        {
            EasyAccessDatabaseInitializer.Initialize();
        }

        protected override string[] ConfigLocations
        {
            get
            {
                return new string[]
                {
                    "assembly://EasyAccess.Repository/EasyAccess.Repository/Bootstrap.Spring.RepositoryConfig.generated.xml",
                    "assembly://EasyAccess.Service/EasyAccess.Service/Bootstrap.Spring.ServiceConfig.xml"
                };
            }
        }

        protected IUnitOfWork EasyAccessUnitOfWork { get; private set; }

        protected AccountRepository AccountRepository { get; private set; }

        protected RoleRepository RoleRepository { get; private set; }

        protected IAccountManageSvc AccountManageService  { get; private set; }

        protected SpringTestBase()
        {
            var appCtx = ContextRegistry.GetContext();
            EasyAccessUnitOfWork = appCtx.GetObject("EasyAccessUnitOfWork") as IUnitOfWork;
            AccountRepository = appCtx.GetObject<AccountRepository>();
            RoleRepository = appCtx.GetObject<RoleRepository>();
            AccountManageService = appCtx.GetObject<IAccountManageSvc>();
        }
    }
}