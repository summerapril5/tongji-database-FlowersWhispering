namespace FlowersWhisperingAPI.User.Models
{
    public class Messenger(int userId, string username, string lastMessage,DateTime sendTime)
    {
        public int UserId { get; set; } = userId;
        public string Username { get; set; } = username;
        public string LastMessage { get; set; } = lastMessage;
        public DateTime SendTime { get; set; } = sendTime;
    }
}