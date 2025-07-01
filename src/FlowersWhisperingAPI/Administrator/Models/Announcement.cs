namespace FlowersWhisperingAPI.Administrator.Models
{
    public class Announcement(int announcementId, string title, string content, int publisherId, DateTime publishedDate)
    {
        public int AnnouncementId { get; set; } = announcementId;
        public string Title { get; set; } = title;
        public string Content { get; set; } = content;
        public int PublisherId { get; set; } = publisherId;
        public DateTime PublishedDate { get; set; } = publishedDate;
    }
}