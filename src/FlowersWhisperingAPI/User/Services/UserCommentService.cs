using FlowersWhisperingAPI.User.Mappers;
using FlowersWhisperingAPI.User.Models;
using FlowersWhisperingAPI.User.Services.Interface;

namespace FlowersWhisperingAPI.User.Services
{
    public class UserCommentService(UserCommentMapper commentMapper,UserAccountMapper accountMapper) : IUserCommentService
    {
        private readonly UserCommentMapper _commentMapper = commentMapper;

        private readonly UserAccountMapper _accountMapper = accountMapper;

        public bool CommentContent(CommentDTO commentDTO)
        {
            return _commentMapper.InsertComment(commentDTO.UserId, commentDTO.PlantId, commentDTO.CommentContent);
        }

        public bool DeleteComment(int commentId)
        {
            return _commentMapper.DeleteComment(commentId);
        }

        public List<Comment> GetAllCommentsOfPlantId(int plantId)
        {
            List<Comment> allComment =  _commentMapper.GetAllComments();
            List<Comment> res = [];
            foreach(Comment comment in allComment)
            {
                if(comment.PlantId == plantId)
                {
                    var username = _accountMapper.GetUsernameByUserId(comment.UserId);
                    if(username == null) 
                        break;
                    else
                        comment.Username = username;
                    res.Add(comment);
                }
            }
            return res;
        }

        public bool UpdateComment(EditCommentDTO editCommentDTO)
        {
            return _commentMapper.UpdateComment(editCommentDTO);
        }
    }

}