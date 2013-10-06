using EasyAccess.Model.DTOs;

namespace EasyAccess.Service.IServices
{
    public interface ILoginService
    {
        bool Login(LoginUser loginUser);

        void Logout();
    }
}