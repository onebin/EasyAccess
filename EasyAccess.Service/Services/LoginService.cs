using System.Web.Security;
using EasyAccess.Infrastructure.Authorization;
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
            var result = false;
            var accountRepository = EasyAccessUnitOfWork.GetRepostory<AccountRepository>();
            var account = accountRepository.VerifyLogin(loginUser);
            if (account != null)
            {
                var token = AuthorizationManager.GetInstance().GetToken(account.Roles);
                FormsAuthentication.SetAuthCookie(token, false);
                result = true;
            }
            return result;
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}