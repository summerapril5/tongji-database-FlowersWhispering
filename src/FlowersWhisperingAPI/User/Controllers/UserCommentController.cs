using FlowersWhisperingAPI.User.Services.Interface;
using FlowersWhisperingAPI.User.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlowersWhisperingAPI.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCommentController(IUserCommentService userCommentService) : ControllerBase
    {
        private readonly IUserCommentService _userCommentService = userCommentService;

        //用户评论
        [HttpPost("comment")]
        public IActionResult CommentContent([FromBody] CommentDTO commentDTO)
        {
            bool result = _userCommentService.CommentContent(commentDTO);
            if (result)
                return Ok("评论成功");
            else
                return BadRequest("评论失败");
        }

        //全部评论
        [HttpGet("all/comments")]
        public IActionResult GetAllComments(int plantId)
        {
            List<Comment> comments = _userCommentService.GetAllCommentsOfPlantId(plantId);
            // if(comments.Count == 0)
            //     return NoContent();
            return Ok(comments);
        }

        //删除评论
        [HttpDelete("delete/comment")]
        public IActionResult DeleteComment(int commentId)
        {
            bool result = _userCommentService.DeleteComment(commentId);
            if (result)
                return Ok("删除成功");
            else
                return BadRequest("删除失败");
        }

        //编辑评论
        [HttpPut("edit/comment")]
        public IActionResult EditComment([FromBody] EditCommentDTO editCommentDTO)
        {
            bool result = _userCommentService.UpdateComment(editCommentDTO);
            if (result)
                return Ok("编辑成功");
            else
                return BadRequest("编辑失败");
        }
    }   
}