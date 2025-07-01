namespace FlowersWhisperingAPI.User.Models
{
    public class MessageDTO
    {
        public string MessageContent { get; set; } = null!;
        public int SenderId { get; set;} = 0;
        public int ReceiverId { get; set;} = 0;
    }
}