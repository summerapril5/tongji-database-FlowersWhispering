namespace FlowersWhisperingAPI.Community.Models
{
    
    public class Commenti(int commentid , int userid , int articleid , string content, DateTime createdDate)
    {
        public int CommentId { get; set; } = commentid;
        public int UserId { get; set; } = userid;
        public int ArticleId { get; set; } = articleid;
        public string Content { get; set; } = content;
        public DateTime CreatedDate { get; set; } = createdDate;
       public string Username { get; set; } = null!;
       public string ArticleTitle { get; set; } = null!;

    }

    
}

