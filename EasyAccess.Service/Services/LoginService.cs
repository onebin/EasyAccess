using System.Data.Entity;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Security;
using EasyAccess.Infrastructure.Authorization;
using EasyAccess.Infrastructure.Constant;
using EasyAccess.Infrastructure.UnitOfWork;
using EasyAccess.Model.DTOs;
using EasyAccess.Model.EDMs;
using EasyAccess.Repository.Configuration;
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
                HttpContext.Current.Session[SessionConst.Token] = token;
                FormsAuthentication.SetAuthCookie(account.Register.LoginUser.UserName,false);
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