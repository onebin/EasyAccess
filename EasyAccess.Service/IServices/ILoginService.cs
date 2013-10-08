using EasyAccess.Infrastructure.UnitOfWork;
using EasyAccess.Model.DTOs;

namespace EasyAccess.Service.IServices
{
    public interface ILoginService
    {
        bool Login(LoginUser loginUser, bool rememberMe = false);

        void Logout();
    }
}