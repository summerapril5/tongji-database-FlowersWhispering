using FlowersWhisperingAPI.User.Mappers;
using FlowersWhisperingAPI.User.Models;
using FlowersWhisperingAPI.User.Services.Interface;

namespace FlowersWhisperingAPI.User.Services
{
    public class UserMessageService(UserMessageMapper messageMapper,UserAccountMapper accountMapper) : IUserMessageService
    {
        private readonly UserAccountMapper _accountMapper = accountMapper;
        private readonly UserMessageMapper _messageMapper = messageMapper;

        public List<Message> GetAllMessages(int senderId, int receiverId)
        {
            var sortedMessages = _messageMapper.GetAllMessages(senderId, receiverId).OrderBy(m => m.SendTime).ToList();
            foreach ( var message in sortedMessages )
            {
                var senderName = _accountMapper.GetUsernameByUserId(message.SenderId);
                var receiverName = _accountMapper.GetUsernameByUserId(message.ReceiverId);
                if (senderName == null || receiverName == null || senderName == "" || receiverName == "")
                    break;
                message.SenderName = senderName;
                message.ReceiverName = receiverName;
            }
            return sortedMessages;
        }

        public List<Messenger> GetMessengers(int userId)
        {
            List<Messenger> messengers = [];
            HashSet<int> messengersId = [];
            List<int> senders = _messageMapper.GetSenders(userId);
            List<int> receivers = _messageMapper.GetReceivers(userId);
            foreach (int sender in senders)
                messengersId.Add(sender);
            foreach (int receiver in receivers)
                messengersId.Add(receiver);
            foreach (int messengerId in messengersId)
            {
                if(messengerId == 0)
                    break;
                var messengerName = _accountMapper.GetUsernameByUserId(messengerId);
                var lastElement = _messageMapper.GetAllMessages(messengerId,userId).LastOrDefault();
                if (messengerName == null || lastElement == null)
                    break;
                Messenger messenger = new(messengerId, messengerName,lastElement.MessageContent,lastElement.SendTime);
                messengers.Add(messenger);   
            }
            messengers = messengers.OrderBy(m => m.SendTime).ToList();
            return messengers;
        }

        public bool SendMessage(MessageDTO messageDTO)
        {
            return _messageMapper.SendMessage(messageDTO);
        }
    }
}