namespace FlowersWhisperingAPI.User.Models
{
    public class Message(int messageId, string messageContent, 
        int senderId, int receiverId, DateTime sendTime,string senderName, string receiverName)
    {
        public int MessageId { get; set; } = messageId;
        public string MessageContent { get; set; } = messageContent;
        public int SenderId { get; set; } = senderId;
        public int ReceiverId { get; set; } = receiverId;
        public string SenderName { get; set; } = senderName;
        public string ReceiverName { get; set;} = receiverName;
        public DateTime SendTime { get; set; } = sendTime;
    }
}