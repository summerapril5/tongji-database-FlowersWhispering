namespace FlowersWhisperingAPI.Administrator.Models
{
    public class ReplyDTO(int userId, string content)
    {
        public int UserId { get; set;} = userId;
        public string Content { get; set;} = content;
    }
}