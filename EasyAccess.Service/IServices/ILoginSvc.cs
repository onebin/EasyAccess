using EasyAccess.Infrastructure.Service;
using EasyAccess.Infrastructure.UnitOfWork;
using EasyAccess.Model.DTOs;
using EasyAccess.Model.VOs;

namespace EasyAccess.Service.IServices
{
    public interface ILoginSvc
    {
        bool Login(LoginUser loginUser, bool rememberMe = false);

        void Logout();
    }
}