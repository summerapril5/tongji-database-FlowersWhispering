using FlowersWhisperingAPI.Community.Models;
using FlowersWhisperingAPI.Community.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FlowersWhisperingAPI.Community.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommuniPostsandCommentsController(ICommuniPostsandCommentsService communiPostsandCommentsService) : ControllerBase
    {
        private readonly ICommuniPostsandCommentsService _communiPostsandCommentsService = communiPostsandCommentsService;

        [HttpPost("comment/add")]
        public void AddComment([FromBody] CommentDTOi commentdto)
        {
            _communiPostsandCommentsService.AddComment(commentdto);
        }

        [HttpPost("post/add")]
        public void AddPost([FromBody] ArticleDTO post)
        {
            _communiPostsandCommentsService.AddPost(post);
        }

        [HttpDelete("comment/delete")]
        public void DeleteComment(int commentId)
        {
            _communiPostsandCommentsService.DeleteComment(commentId);
        }

        [HttpDelete("post/delete")]
        public void DeletePost(int postId)
        {
            _communiPostsandCommentsService.DeletePost(postId);
        }
        //某个帖子的所有评论
        [HttpGet("comment/all")]
        public IActionResult GetAllComments(int postId)
        {
            return Ok(_communiPostsandCommentsService.GetAllComments(postId));
        }

        [HttpGet("comment/user/all")]
        public IActionResult GetAllCommentsByUserId(int userId)
        {
            return Ok(_communiPostsandCommentsService.GetAllCommentsByUserId(userId));
        }

        [HttpGet("post/all")]
        public IActionResult GetAllPosts()
        {
            return Ok(_communiPostsandCommentsService.GetAllPosts());
        }

        [HttpGet("post/user/all")]
        public IActionResult GetAllPostsByUserId(int userId)
        {
            return Ok(_communiPostsandCommentsService.GetAllPostsByUserId(userId));
        }

        [HttpGet("post/all/search")]
        public IActionResult GetAllPostsByTitleOrContent(string text)
        {
            return Ok(_communiPostsandCommentsService.GetAllPostsByTitleOrContent(text));
        }

        [HttpPut("post/edit")]
        public void UpdatePost([FromBody] Article post)
        {
            _communiPostsandCommentsService.UpdatePost(post);
        }

    }
}