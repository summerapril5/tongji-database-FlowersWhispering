namespace FlowersWhisperingAPI.Administrator.Models
{
    public class Reply(int id, int userId, string content, DateTime replyDate)
    {

        public int Id { get; set;} = id;
        public int UserId { get; set;} = userId;
        public string Content { get; set;} = content;
        public DateTime ReplyDate { get; set;} = replyDate;
    }
}