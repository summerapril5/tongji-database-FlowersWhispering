namespace FlowersWhisperingAPI.Community.Models
{
    public class Favor(int favorId,int userId, int articleId, string title, string content, int publisherId, DateTime publishedDate)
    {
        public int FavorId { get; set; } = favorId;
        public int UserId { get; set; } = userId;
        public int ArticleId { get; set; } = articleId;
        public string Title { get; set; } = title;
        public string Content { get; set; } = content;
        public int PublisherId { get; set; } = publisherId;
        public DateTime PublishedDate { get; set; } = publishedDate;
     
    }
}
