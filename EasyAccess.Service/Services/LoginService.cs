using System;
using System.Linq;
using EasyAccess.Authorization;
using EasyAccess.Infrastructure.Service;
using EasyAccess.Infrastructure.Util;
using EasyAccess.Infrastructure.Util.ConditionBuilder;
using EasyAccess.Model.DTOs;
using EasyAccess.Model.EDMs;
using EasyAccess.Repository.IRepositories;
using EasyAccess.Service.IServices;

namespace EasyAccess.Service.Services
{
    public class LoginService : ServiceBase, ILoginService
    {
        public IAccountRepository AccountRepository { get; set; }

        public bool Login(LoginUser loginUser, bool rememberMe = false)
        {
            var result = false;
            var account = AccountRepository.VerifyLogin(loginUser);
            if (account != null)
            {
                var authMgr = AuthorizationManager.GetInstance();
                var token = authMgr.GetToken(account.Roles, AccountRepository.GetPermissions(account.Id));
                authMgr.SetTicket(loginUser.UserName, token, rememberMe);
                account.Register.LastLoginIP = IPAddress.GetIPAddress();
                account.Register.LastLoginTime = DateTime.Now;
                base.UnitOfWork.Commit();
                result = true;
            }
            return result;
        }

        public void Logout()
        {
            AuthorizationManager.GetInstance().ClearTicket();
        }
    }
}