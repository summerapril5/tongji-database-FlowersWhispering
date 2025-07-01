using FlowersWhisperingAPI.User.Models;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;

namespace FlowersWhisperingAPI.User.Mappers
{
    public class UserMessageMapper(string connectionString)
    {
        private readonly string _connectionString = connectionString;

        public bool SendMessage(MessageDTO messageDTO)
        {
            string sql = "INSERT INTO messages (sender_id, receiver_id, message_content) VALUES (:SenderId, :ReceiverId, :MessageContent)";
            try
            {
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                using var command = new OracleCommand(sql, connection);
                command.Parameters.Add(":SenderId", OracleDbType.Int32).Value = messageDTO.SenderId;
                command.Parameters.Add(":ReceiverId", OracleDbType.Int32).Value = messageDTO.ReceiverId;
                command.Parameters.Add(":MessageContent", OracleDbType.Varchar2).Value = messageDTO.MessageContent;
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
               Console.WriteLine($"Error send: {ex.Message}");
               return false; // 插入失败
            }           
        }

        public List<int> GetSenders(int userId)
        {
            List<int> senders = [];
            string sql = "SELECT DISTINCT sender_id FROM messages WHERE receiver_id = :UserId";
            try
            {
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                using var command = new OracleCommand(sql, connection);
                command.Parameters.Add(":UserId", OracleDbType.Int32).Value = userId;
                using var reader = command.ExecuteReader();
                while(reader.Read())
                    senders.Add(reader.GetInt32(0));
            }
            catch (Exception ex)
            {
               Console.WriteLine($"Error GetSenders: {ex.Message}");
            }
            return senders;
        }

        public List<int> GetReceivers(int userId)
        {
            List<int> receivers = [];
            string sql = "SELECT DISTINCT receiver_id FROM messages WHERE sender_id = :SenderId";

            try
            {
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                using var command = new OracleCommand(sql, connection);
                command.Parameters.Add(":SenderId", OracleDbType.Int32).Value = userId;
                using var reader = command.ExecuteReader();
                while (reader.Read())
                    receivers.Add(reader.GetInt32(0));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to retrieve receivers: {ex.Message}");
            }

            return receivers;
        }
        
        public List<Message> GetAllMessages(int senderId, int receiverId)
        {
            List<Message> messages = [];
            string sql = "SELECT * FROM messages WHERE (sender_id = :SenderId AND receiver_id = :ReceiverId) OR (sender_id = :ReceiverId AND receiver_id = :SenderId)";
            try
            {
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                using var command = new OracleCommand(sql, connection);
                command.Parameters.Add(":SenderId", OracleDbType.Int32).Value = senderId;
                command.Parameters.Add(":ReceiverId", OracleDbType.Int32).Value = receiverId;
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int messageId = reader.GetInt32(0);
                    int _senderId = reader.GetInt32(1);
                    int _receiverId = reader.GetInt32(2);
                    string messageContent = reader.GetString(3);
                    DateTime messageTime = reader.GetDateTime(4);
                    messages.Add(new Message(messageId, messageContent, _senderId, _receiverId, messageTime,"",""));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to retrieve messages: {ex.Message}");
            }
            return messages;
        }
    }
}