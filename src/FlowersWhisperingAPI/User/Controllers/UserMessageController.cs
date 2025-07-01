using FlowersWhisperingAPI.User.Services.Interface;
using FlowersWhisperingAPI.User.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlowersWhisperingAPI.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMessageController(IUserMessageService userMessageService) : ControllerBase
    {
        private readonly IUserMessageService _userMessageService = userMessageService;

        //发私信
        [HttpPost("message")]
        public IActionResult SendMessage([FromBody] MessageDTO messageDTO)
        {
            bool result = _userMessageService.SendMessage(messageDTO);
            if (result)
                return Ok("私信成功");
            else
                return BadRequest("私信失败");
        }

        //获取和某人的私信
        [HttpGet("all/messages")]
        public IActionResult GetAllMessages(int userId,int otherId)
        {
            List<Message> messages = _userMessageService.GetAllMessages(userId, otherId);
            return Ok(messages);
        }

        //获取私信列表
        [HttpGet("messages")]
        public IActionResult GetMessengers(int userId)
        {
            List<Messenger> messages = _userMessageService.GetMessengers(userId);
            return Ok(messages);
        }
    }   
}