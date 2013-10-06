using EasyAccess.Infrastructure.UnitOfWork;
using EasyAccess.Model.DTOs;
using EasyAccess.Model.EDMs;
using EasyAccess.Repository.Repositories;
using EasyAccess.Service.IServices;

namespace EasyAccess.Service.Services
{
    public class LoginService : ILoginService
    {
        public IUnitOfWork EasyAccessUnitOfWork { get; set; }

        public bool Login(LoginUser loginUser)
        {
            var accountRepository = EasyAccessUnitOfWork.GetRepostory<AccountRepository>();
            var account = accountRepository.VerifyLogin(loginUser);
            throw new System.NotImplementedException();
        }

        public void Logout()
        {
            throw new System.NotImplementedException();
        }
    }
}