using System.Data;
using FlowersWhisperingAPI.Administrator.Models;
using Oracle.ManagedDataAccess.Client;

namespace FlowersWhisperingAPI.Administrator.Mappers
{
    public class AdminAnnoMapper(string connectionString)
    {
        private readonly string _connectionString = connectionString;

        public List<Announcement> GetAllAnnouncements()
        {
            List<Announcement> announcements = [];
            string sql = "SELECT * FROM Articles WHERE article_type = 'announcement'";
            try
            {
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                using var command = new OracleCommand(sql, connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string title = reader.GetString(1);
                    string content = reader.GetString(2);
                    int pid = reader.GetInt32(3);
                    DateTime date = reader.GetDateTime(4);
                    announcements.Add(new Announcement(id, title, content, pid, date));
                }
                return announcements;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return [];
            }
        }

        public void AddAnnouncement(AnnoDTO annoDTO)
        {
            string sql = @"INSERT INTO Articles (article_title, article_content, publisher_id, 
                article_type) VALUES (:Title, :Content, :PublisherId, 'announcement')";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":Title", OracleDbType.Varchar2).Value = annoDTO.Title;
            command.Parameters.Add(":Content", OracleDbType.Clob).Value = annoDTO.Content;
            command.Parameters.Add(":PublisherId", OracleDbType.Int32).Value = annoDTO.PublisherId;
            command.ExecuteNonQuery(); 
        } 

        public void DeleteAnnouncement(int id)
        {
            string sql = "DELETE FROM Articles WHERE article_id = :ArticleId AND article_type = 'announcement'";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":ArticleId", OracleDbType.Int32).Value = id;
            command.ExecuteNonQuery();  
        }    
    }
}