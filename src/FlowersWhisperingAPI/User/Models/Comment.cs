namespace FlowersWhisperingAPI.User.Models
{
    public class Comment(int commentId, int userId, int plantId, string commentContent, DateTime commentTime)
    {
        public int CommentId { get; set; } = commentId;
        public int UserId { get; set; } = userId;
        public int PlantId { get; set; } = plantId;
        public string CommentContent { get; set; } = commentContent;
        public DateTime CommentTime { get; set; } = commentTime;
        public string Username { get; set; } = null!;
    }
}