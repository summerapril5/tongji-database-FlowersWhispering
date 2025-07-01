using FlowersWhisperingAPI.Community.Mappers;
using FlowersWhisperingAPI.Community.Models;
using FlowersWhisperingAPI.Community.Services.Interface;

namespace FlowersWhisperingAPI.Community.Services
{
    public class CommuniPostsandCommentsService(CommuniPostsandCommentsMapper postsandcommentsMapper) : ICommuniPostsandCommentsService
    {
        private readonly CommuniPostsandCommentsMapper _postsandcommentsMapper = postsandcommentsMapper;
        //private readonly CommuniScoreMapper _scoreMapper = scoreMapper;
        public List<Article> GetAllPosts()
        {
            return _postsandcommentsMapper.GetAllPosts();
        }
        public List<Article> GetAllPostsByUserId(int userId)
        {
            return _postsandcommentsMapper.GetAllPostsByUserId(userId);
        }
        public List<Article> GetAllPostsByTitleOrContent(string text)
        {
            return _postsandcommentsMapper.GetAllPostsByTitleOrContent(text);
        }
        public void AddPost(ArticleDTO post)
        {
            _postsandcommentsMapper.AddPost(post);
        }
        public void DeletePost(int postId)
        {
            _postsandcommentsMapper.DeletePost(postId);
        }
        public void UpdatePost(Article post)
        {
            _postsandcommentsMapper.UpdatePost(post);
        }
        public List<Commenti> GetAllComments(int postId)
        {
            return _postsandcommentsMapper.GetAllComments(postId);
        }
        public List<Commenti> GetAllCommentsByUserId(int userId)
        {
            return _postsandcommentsMapper.GetAllCommentsByUserId(userId);
        }
        public void AddComment(CommentDTOi commentdto)
        {
            _postsandcommentsMapper.AddComment(commentdto);
        }
        public void DeleteComment(int commentId)
        {
            _postsandcommentsMapper.DeleteComment(commentId);
        }


    }
}
