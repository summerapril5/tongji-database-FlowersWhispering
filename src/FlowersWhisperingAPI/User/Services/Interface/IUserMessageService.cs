using FlowersWhisperingAPI.User.Models;

namespace FlowersWhisperingAPI.User.Services.Interface
{
    public interface IUserMessageService
    {
        public bool SendMessage(MessageDTO messageDTO);
        public List<Message> GetAllMessages(int senderId, int receiverId);
        List<Messenger> GetMessengers(int userId);
    }
}