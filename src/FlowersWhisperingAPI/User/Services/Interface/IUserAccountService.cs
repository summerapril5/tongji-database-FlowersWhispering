using FlowersWhisperingAPI.User.Models;

namespace FlowersWhisperingAPI.User.Services.Interface
{
    public interface IUserAccountService
    {
        string GetPasswordByUsername(string username);
        string GetPasswordByEmail(string email);
        bool Register(UserInfo userRegister);
        bool UserUpdate(UserDTO registerDTO);
        bool IsHaveUsername(string username);
        bool IsHaveEmail(string email);
        int GetUserIdByUsername(string username);
        int GetUserIdByEmail(string email);
        DateTime GetRegistrationTimeById(int id);
        UserInfo? GetUserInfoById(int id);
    }

}