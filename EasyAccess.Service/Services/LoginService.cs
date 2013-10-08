using System;
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
                var mgr = AuthorizationManager.GetInstance();
                var token = mgr.GetToken(account.Roles, accountRepository.GetPermissions(account.Id));
                HttpContext.Current.Session[SessionConst.Token] = token;
                var ticket = new FormsAuthenticationTicket(
                    1,
                    loginUser.UserName,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    false,
                    token
                );
                var hashTicket = FormsAuthentication.Encrypt(ticket) ;
                var userCookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashTicket);
                HttpContext.Current.Response.Cookies.Add(userCookie);
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