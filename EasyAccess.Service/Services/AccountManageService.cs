using EasyAccess.Infrastructure.Service;
using EasyAccess.Model.EDMs;
using EasyAccess.Repository.IRepositories;
using EasyAccess.Service.IServices;

namespace EasyAccess.Service.Services
{
    public class AccountManageService : ServiceBase, IAccountManageService
    {
        public IAccountRepository AccountRepository { get; set; }
    }
}
