using FlowersWhisperingAPI.User.Services.Interface;
using FlowersWhisperingAPI.User.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlowersWhisperingAPI.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController(IUserAccountService userAccountService) : ControllerBase
    {
        private readonly IUserAccountService _userAccountService = userAccountService;

        //用户登录
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            if(_userAccountService.IsHaveUsername(loginDTO.Username)){
                string password = _userAccountService.GetPasswordByUsername(loginDTO.Username);
                if(password == loginDTO.Password)
                {
                    int id = _userAccountService.GetUserIdByUsername(loginDTO.Username);                   
                    var userInfo = _userAccountService.GetUserInfoById(id);
                    if(userInfo == null)
                        return BadRequest("未查到此账户，请先注册");
                    if(userInfo.UserStatus == "banned")
                        return BadRequest("账户已被封禁");
                    DateTime registrationTime = _userAccountService.GetRegistrationTimeById(id);
                    return Ok(new
                    {
                        message = "登录成功",
                        userId = id,
                        username = loginDTO.Username,
                        email = userInfo.Email,
                        password = userInfo.Password,
                        languagePreference = userInfo.LanguagePreference,
                        userState = userInfo.UserStatus,
                        userRole = userInfo.UserRole,
                        bio = userInfo.Bio,
                        gender = userInfo.Gender,
                        avatar = userInfo.Avatar,
                        registrationTime,
                    });
                }
                else
                    return Unauthorized("密码错误");
            }else if(_userAccountService.IsHaveEmail(loginDTO.Username)){
                string password = _userAccountService.GetPasswordByEmail(loginDTO.Username);//把传来的用户名作为邮箱
                if(password == loginDTO.Password)
                {
                    int id = _userAccountService.GetUserIdByEmail(loginDTO.Username);                   
                    var userInfo = _userAccountService.GetUserInfoById(id);
                    if(userInfo == null)
                        return BadRequest("未查到此账户，请先注册");
                    if(userInfo.UserStatus == "banned")
                        return BadRequest("账户已被封禁");
                    DateTime registrationTime = _userAccountService.GetRegistrationTimeById(id);
                    return Ok(new
                    {
                        message = "登录成功",
                        userId = id,
                        username = loginDTO.Username,
                        email = userInfo.Email,
                        password = userInfo.Password,
                        languagePreference = userInfo.LanguagePreference,
                        userState = userInfo.UserStatus,
                        userRole = userInfo.UserRole,
                        bio = userInfo.Bio,
                        gender = userInfo.Gender,
                        registrationTime,
                    });      
                }                    
                else
                    return Unauthorized("密码错误");
            }
            return BadRequest("未查到此账户，请先注册");
        }

        //用户注册
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDTO userDTO)
        {
            if (_userAccountService.IsHaveUsername(userDTO.Username))
                return BadRequest("注册失败，用户名已有人使用");
            if (_userAccountService.IsHaveEmail(userDTO.Email))
                return BadRequest("注册失败，邮箱已有人使用");
            string username = userDTO.Username;
            string password = userDTO.Password;
            string email = userDTO.Email;
            string languagePreference = userDTO.LanguagePreference;
            string bio = userDTO.Bio;
            string avatar = userDTO.Avatar;
            string gender = userDTO.Gender;
            UserInfo userRegister = new UserInfo(username, password, email, languagePreference,bio,avatar,gender);
            bool result = _userAccountService.Register(userRegister);
            if (result)
                return Ok("注册成功");
            else
                return BadRequest("注册失败");
       }

       //用户资料编辑
       [HttpPut("edit/user")]
       public IActionResult UserEdit([FromBody] UserDTO userDTO)
       {
            if (_userAccountService.IsHaveUsername(userDTO.Username) &&
                    _userAccountService.GetUserIdByUsername(userDTO.Username) != userDTO.UserId)
                return BadRequest("编辑失败，用户名已有人使用");
            if (_userAccountService.IsHaveEmail(userDTO.Email) && 
                    _userAccountService.GetUserIdByEmail(userDTO.Email) != userDTO.UserId)
                return BadRequest("编辑失败，邮箱已有人使用");
            bool result = _userAccountService.UserUpdate(userDTO);
            if (result)
                return Ok("编辑成功");
            else
                return BadRequest("编辑失败");
       }
    }
}