﻿using System.Collections.Generic;
using EasyAccess.Infrastructure.Repository;
using EasyAccess.Model.DTOs;
using EasyAccess.Model.EDMs;

namespace EasyAccess.Repository.IRepositories
{
    public interface IAccountRepository: IRepositoryBase<Account, long>
    {
        ICollection<Role> GetRoles(long accountId);

        ICollection<Permission> GetPermissions(long accountId);

        ICollection<Menu> GetMenus(long accountId);

        Register GetRegister(string userName);

        Account VerifyLogin(LoginUser loginUser);

        void ResetPasswork(LoginUser loginUser);
    }
}
