using System;
using System.Data.Entity;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Security;
using EasyAccess.Infrastructure.Authorization;
using EasyAccess.Infrastructure.Constant;
using EasyAccess.Infrastructure.UnitOfWork;
using EasyAccess.Infrastructure.Util;
using EasyAccess.Model.DTOs;
using EasyAccess.Model.EDMs;
using EasyAccess.Repository.Configuration;
using EasyAccess.Repository.Repositories;
using EasyAccess.Service.IServices;

namespace EasyAccess.Service.Services
{
    public class LoginService : ILoginService
    {
        public bool Login(LoginUser loginUser, bool rememberMe = false)
        {
            var result = false;
            using (IUnitOfWork easyAccessUnitOfWork = new UnitOfWorkBase(new EasyAccessContext()))
            {
                var accountRepository = easyAccessUnitOfWork.GetRepostory<AccountRepository>();
                var account = accountRepository.VerifyLogin(loginUser);
                if (account != null)
                {
                    var authMgr = AuthorizationManager.GetInstance();
                    var token = authMgr.GetToken(account.Roles, accountRepository.GetPermissions(account.Id));
                    authMgr.SetTicket(loginUser.UserName, token, rememberMe);
                    account.Register.LastLoginIP = IPAddress.GetIPAddress();
                    account.Register.LastLoginTime = DateTime.Now;
                    easyAccessUnitOfWork.Commit();
                    result = true;
                }
            }
            return result;
        }

        public void Logout()
        {
            AuthorizationManager.GetInstance().ClearTicket();
        }
    }
}