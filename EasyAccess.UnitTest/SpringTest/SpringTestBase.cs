using EasyAccess.Infrastructure.UnitOfWork;
using EasyAccess.Repository.IRepositories;
using Spring.Context.Support;
using Spring.Testing.Microsoft;

namespace EasyAccess.UnitTest.SpringTest
{
    public abstract class SpringTestBase : AbstractDependencyInjectionSpringContextTests
    {
        protected override string[] ConfigLocations
        {
            get
            {
                return new string[]
                {
                    "assembly://EasyAccess.Repository/EasyAccess.Repository/SpringConfig.RepositoryConfig.xml"
                };
            }
        }

        protected IUnitOfWork EasyAccessUnitOfWork { get; private set; }

        protected IAccountRepository AccountRepository { get; private set; }

        protected IRoleRepository RoleRepository { get; private set; }

        protected SpringTestBase()
        {
            var appCtx = ContextRegistry.GetContext();
            EasyAccessUnitOfWork = appCtx.GetObject("EasyAccessUnitOfWork") as IUnitOfWork;
            AccountRepository = appCtx.GetObject("AccountRepository") as IAccountRepository;
            RoleRepository = appCtx.GetObject("RoleRepository") as IRoleRepository;
        }
    }
}
