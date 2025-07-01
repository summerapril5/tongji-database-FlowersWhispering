using FlowersWhisperingAPI.Administrator.Models;
using FlowersWhisperingAPI.Administrator.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FlowersWhisperingAPI.Administrator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserController(IAdminUserService adminUserService) : ControllerBase
    {
        private readonly IAdminUserService _adminUserService = adminUserService;

        [HttpPut("ban")]
        public void BanUser(int id)
        {
           _adminUserService.ChangeUserState(id,"banned");
        }
        [HttpPut("unblock")]
        public void NoBanUser(int id)
        {
           _adminUserService.ChangeUserState(id,"active");
        }

        [HttpPost("user/feedback")]
        public IActionResult Feedback([FromBody] FeedbackDTO feedbackDTO)
        {
            if(feedbackDTO.FeedbackContent != null && _adminUserService.Feedback(feedbackDTO.UserId,feedbackDTO.FeedbackContent))
                return Ok("反馈成功");
            else
                return BadRequest("反馈失败");          
        }

        [HttpGet("all/feedback")]
        public IActionResult GetAllFeedback()
        {
            return Ok(_adminUserService.GetAllFeedback().OrderBy(x => x.FeedbackId).ToList());
        }

        [HttpPost("reply")]
        public void Reply([FromBody] ReplyDTO replyDTO)
        {
            _adminUserService.Reply(replyDTO);
        }

        [HttpGet("all/replies")]
        public IActionResult GetAllReplies(int userId)
        {
            return Ok(_adminUserService.GetReplies(userId).OrderBy(x => x.Id).ToList());
        }


        [HttpGet("all/user")]
        public IActionResult GetAllUsers()
        {
            return Ok(_adminUserService.GetAllUsers());
        }
    }
}