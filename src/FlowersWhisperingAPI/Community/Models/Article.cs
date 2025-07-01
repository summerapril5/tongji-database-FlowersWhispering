namespace FlowersWhisperingAPI.Community.Models
{
    public class Article(int articleId, string title, string content, int publisherId, DateTime publishedDate)
    {
        public int ArticleId { get; set; } = articleId;
        public string Title { get; set; } = title;
        public string Content { get; set; } = content;
        public int PublisherId { get; set; } = publisherId;
        public DateTime PublishedDate { get; set; } = publishedDate;
        public string Username { get; set; } = null!;
    }
    
}