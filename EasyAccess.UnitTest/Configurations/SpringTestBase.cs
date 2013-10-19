using EasyAccess.Infrastructure.UnitOfWork;
using EasyAccess.Repository.IRepositories;
using EasyAccess.Service.IServices;
using Spring.Context.Support;
using Spring.Testing.Microsoft;

namespace EasyAccess.UnitTest.Configurations
{
    public abstract class SpringTestBase : AbstractDependencyInjectionSpringContextTests
    {
        protected override string[] ConfigLocations
        {
            get
            {
                return new string[]
                {
                    "assembly://EasyAccess.Repository/EasyAccess.Repository/Configurations.SpringConfig.RepositoryConfig.xml",
                    "assembly://EasyAccess.Service/EasyAccess.Service/Configurations.SpringConfig.ServiceConfig.xml"
                };
            }
        }

        protected IUnitOfWork EasyAccessUnitOfWork { get; private set; }

        protected IAccountRepository AccountRepository { get; private set; }

        protected IRoleRepository RoleRepository { get; private set; }

        protected IAccountManageService AccountManageService  { get; private set; }

        protected SpringTestBase()
        {
            var appCtx = ContextRegistry.GetContext();
            EasyAccessUnitOfWork = appCtx.GetObject("EasyAccessUnitOfWork") as IUnitOfWork;
            AccountRepository = appCtx.GetObject<IAccountRepository>();
            RoleRepository = appCtx.GetObject<IRoleRepository>();
            AccountManageService = appCtx.GetObject<IAccountManageService>();
        }
    }
}