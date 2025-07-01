using System.Data;
using FlowersWhisperingAPI.Administrator.Models;
using Oracle.ManagedDataAccess.Client;

namespace FlowersWhisperingAPI.Administrator.Mappers
{
    public class AdminUserMapper(string connectionString) 
    {
        private readonly string _connectionString = connectionString;

        public void ChangeUserState(int userId,string state)
        {
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand("UPDATE USERS SET user_status = :State WHERE USER_ID = :Id", connection);
            command.Parameters.Add(":State", OracleDbType.Varchar2).Value = state;
            command.Parameters.Add(":Id", OracleDbType.Int32).Value = userId;
            command.ExecuteNonQuery();
        }

        public bool Feedback(int userId,string feedback)
        {
            try
            {
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                using var command = new OracleCommand("INSERT INTO FEEDBACKS (USER_ID, FEEDBACK_CONTENT) VALUES (:Id, :Feedback)", connection);
                command.Parameters.Add(":Id", OracleDbType.Int32).Value = userId;
                command.Parameters.Add(":Feedback", OracleDbType.Varchar2).Value = feedback;
                command.ExecuteNonQuery();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            
        }

        public void Reply(int userId, string content){
            try
            {
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                using var command = new OracleCommand("INSERT INTO REPLIES (USER_ID, REPLY_CONTENT) VALUES (:Id, :Content)", connection);
                command.Parameters.Add(":Id", OracleDbType.Int32).Value = userId;
                command.Parameters.Add(":Content", OracleDbType.Varchar2).Value = content;
                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<Reply> GetReplies(int userId)
        {
            List<Reply> replies = [];
            try
            {
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                using var command = new OracleCommand("SELECT * FROM REPLIES WHERE USER_ID = :Id", connection);
                command.Parameters.Add(":Id", OracleDbType.Int32).Value = userId;
                var read = command.ExecuteReader();
                while (read.Read())
                {
                    int id  = read.GetInt32(0);
                    int _userId = read.GetInt32(1);
                    string content = read.GetString(2);
                    DateTime time = read.GetDateTime(3);
                    replies.Add(new Reply(id,_userId,content,time));
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return replies;
        }

        public List<Feedback> GetAllFeedback()
        {
            List<Feedback> feedbacks = [];
            try
            {
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                using var command = new OracleCommand("SELECT * FROM FEEDBACKS", connection);
                var read = command.ExecuteReader();
                while (read.Read())
                {
                    int id = read.GetInt32(0);
                    int userId = read.GetInt32(1);
                    string content = read.GetString(2);
                    DateTime time = read.GetDateTime(3);
                    feedbacks.Add(new Feedback(id, userId,content,time));
                }
                return feedbacks;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return [];
            }
        }
        public List<UserAdmin> GetAllUsers()
        {
            List<UserAdmin> users = [];
            string sql="SELECT * FROM users";
            try
            {
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                using var command = new OracleCommand(sql, connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(reader.GetOrdinal("user_id"));
                    string uname = reader.GetString(reader.GetOrdinal("username"));
                    string uemail = reader.GetString(reader.GetOrdinal("email"));
                    string ustatus = reader.GetString(reader.GetOrdinal("user_status"));
                    string urole = reader.GetString(reader.GetOrdinal("user_role"));
                    users.Add(new UserAdmin(id, uname,uemail,ustatus,urole));
                }
                return users;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return [];
            }
        }
    }
}