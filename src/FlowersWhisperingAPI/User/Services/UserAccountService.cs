using FlowersWhisperingAPI.User.Mappers;
using FlowersWhisperingAPI.User.Models;
using FlowersWhisperingAPI.User.Services.Interface;

namespace FlowersWhisperingAPI.User.Services
{
    public class UserAccountService(UserAccountMapper accountMapper) : IUserAccountService
    {
        private readonly UserAccountMapper _accountMapper = accountMapper;

        public string GetPasswordByUsername(string username)
        {
            try
            {
                var user = _accountMapper.GetUserByUsername(username);
                if(user == null)
                    return "";
                //可以加密下
                return user.Password;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during login: {ex.Message}");
                return "";
            }
        }

        public string GetPasswordByEmail(string email)
        {
            try
            {
                var user = _accountMapper.GetUserByEmail(email);
                if(user == null)
                    return "";
                //可以加密下
                return user.Password;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during login: {ex.Message}");
                return "";
            }
        }

       public bool IsHaveEmail(string email)
        {
            bool result = _accountMapper.IsHaveEmail(email);
            return result;
        }

        public bool IsHaveUsername(string username)
        {
            bool result = _accountMapper.IsHaveUsername(username);
            return result;
        }

        public bool Register(UserInfo userInfo)
        {
            try
            {
                bool result = _accountMapper.InsertUser(userInfo);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during registration: {ex.Message}");
                return false;
            }
        }

        public bool UserUpdate(UserDTO registerDTO)
        {
            try
            {
                bool result = _accountMapper.UserUpdate(registerDTO);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during registration: {ex.Message}");
                return false;
            }
        }

        public int GetUserIdByUsername(string username)
        {
            var user = _accountMapper.GetUserByUsername(username);
            return user != null ? user.UserId : 0;
        }

        public int GetUserIdByEmail(string email)
        {
            var user = _accountMapper.GetUserByEmail(email);
            return user != null ? user.UserId : 0;
        }

        public UserInfo? GetUserInfoById(int id)
        {
            return _accountMapper.GetUserById(id);
        }

        public DateTime GetRegistrationTimeById(int id)
        {
            return _accountMapper.GetRegistrationTimeById(id);
        }
    }
}