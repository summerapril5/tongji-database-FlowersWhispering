using System.Data;
using FlowersWhisperingAPI.Community.Models;
using Oracle.ManagedDataAccess.Client;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FlowersWhisperingAPI.Community.Mappers
{
    public class CommuniPostsandCommentsMapper(string connectionString)
    {
        private readonly string _connectionString = connectionString;

        public string? GetUsernameByUserId(int userId)
        {
            string sql = "SELECT username FROM users WHERE user_id = :UserId";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":UserId", OracleDbType.Int32).Value = userId;
            var result = command.ExecuteScalar();
            return result != null && result != DBNull.Value ? result.ToString(): null;                       
        }  
        public string? GetArticleTitleById(int articleId)
        {
            string sql = "SELECT article_title FROM Articles WHERE article_id = :ArticleId";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":ArticleId", OracleDbType.Int32).Value = articleId;
            var result = command.ExecuteScalar();
            return result != null && result != DBNull.Value ? result.ToString(): null;                       
        }  
        public void AddComment(CommentDTOi commentdto)
        {
            string sql = @"INSERT INTO Comments (user_id, article_id, comment_content 
            ) VALUES (:UserId, :ArticleId, :Contentd)";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":UserId", OracleDbType.Int32).Value = commentdto.UserId;
            command.Parameters.Add(":ArticleId", OracleDbType.Int32).Value = commentdto.ArticleId;
            command.Parameters.Add(":Contentd", OracleDbType.Clob).Value = commentdto.Content;
            command.ExecuteNonQuery();

        }
        public void AddPost(ArticleDTO post)
        {
            string sql = @"INSERT INTO Articles (article_title, article_content, publisher_id, 
                article_type) VALUES (:Title, :Content, :PublisherId, 'article')";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":Title", OracleDbType.Varchar2).Value = post.Title;
            command.Parameters.Add(":Content", OracleDbType.Clob).Value = post.Content;
            command.Parameters.Add(":PublisherId", OracleDbType.Int32).Value = post.PublisherId;
            command.ExecuteNonQuery(); 
        }
        public void DeleteComment(int commentId)
        {
            string sql = "DELETE FROM Comments WHERE comment_id = :commentId";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":commentId", OracleDbType.Int32).Value = commentId;
            command.ExecuteNonQuery();  

        }
        public void DeletePost(int postId)
        {
            string sql = "DELETE FROM Articles WHERE article_id = :ArticleId AND article_type = 'article'";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":ArticleId", OracleDbType.Int32).Value = postId;
            command.ExecuteNonQuery();  

        }
        //帖子的所有评论
        public List<Commenti> GetAllComments(int postId)
        {
            List<Commenti> comments = [];
            string sql = "SELECT * FROM Comments WHERE article_id = :PostId";
            try
            {
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                using var command = new OracleCommand(sql, connection);
                command.Parameters.Add(":PostId", OracleDbType.Int32).Value = postId;
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int commentId = reader.GetInt32(reader.GetOrdinal("comment_id"));
                    int userId = reader.GetInt32(reader.GetOrdinal("user_id"));
                    int articleId = reader.GetInt32(reader.GetOrdinal("article_id"));
                    string commentContent = reader.GetString(reader.GetOrdinal("comment_content"));
                    DateTime createdDate = reader.GetDateTime(reader.GetOrdinal("created_date"));
                    string? username = GetUsernameByUserId(userId);
                    string? articletitle = GetArticleTitleById(articleId);
                    // 创建 Commenti 对象
                    Commenti comment = new Commenti(commentId, userId, articleId, commentContent, createdDate)
                    {
                        Username = username ?? "Unknown",  // 处理空用户名
                        ArticleTitle = articletitle ?? "Unknown"

                    };
                    comments.Add(comment);
                }
                return comments;
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error occurred: {e.Message}");
                return [];
            }
          
        }
        //用户的所有评论
        public List<Commenti> GetAllCommentsByUserId(int userId)
        {
            List<Commenti> comments = [];
            string sql = "SELECT * FROM Comments WHERE user_id = :UserId";
            try
            {
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                using var command = new OracleCommand(sql, connection);
                command.Parameters.Add(":UserId", OracleDbType.Int32).Value = userId;
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int commentId = reader.GetInt32(reader.GetOrdinal("comment_id"));
                    int uId = reader.GetInt32(reader.GetOrdinal("user_id"));
                    int articleId = reader.GetInt32(reader.GetOrdinal("article_id"));
                    string commentContent = reader.GetString(reader.GetOrdinal("comment_content"));
                    DateTime createdDate = reader.GetDateTime(reader.GetOrdinal("created_date"));
                    string? username = GetUsernameByUserId(userId);
                    string? articletitle = GetArticleTitleById(articleId);
                    // 创建 Commenti 对象
                    Commenti comment = new Commenti(commentId, uId, articleId, commentContent, createdDate)
                    {
                        Username = username ?? "Unknown",
                        ArticleTitle = articletitle ?? "Unknown"  
                    };
                    comments.Add(comment);
                }
                return comments;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occurred: {e.Message}");
                return [];
            }

        }

        public List<Article> GetAllPosts()
        {
            List<Article> posts = [];
            string sql = "SELECT * FROM Articles WHERE article_type = 'article'";
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
                    string? username = GetUsernameByUserId(pid);
                    if (username == null)
                    {
                       throw new Exception($"Username not found for userId: {pid}");
                    }
                    posts.Add(new Article(id, title, content, pid, date){ Username = username });
                }
                return posts;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return [];
            }
        }
        public List<Article> GetAllPostsByUserId(int userId)
        {
            List<Article> posts = [];
            string sql = "SELECT * FROM Articles WHERE publisher_id = :userId";
            try
            {
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                using var command = new OracleCommand(sql, connection);
                command.Parameters.Add(":userId", OracleDbType.Int32).Value = userId;
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {                  
                    int id = reader.GetInt32(0);
                    string title = reader.GetString(1);
                    string content = reader.GetString(2);
                    int pid = reader.GetInt32(3);
                    DateTime date = reader.GetDateTime(4);
                    posts.Add(new Article(id, title, content, pid, date));
                }
                return posts;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return [];
            }          

        }
        public List<Article> GetAllPostsByTitleOrContent(string text)
        {
            List<Article> posts = [];
            string sql = "SELECT * FROM Articles WHERE article_title LIKE :text OR article_content LIKE :text";
            try
            {
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                using var command = new OracleCommand(sql, connection);
                // 绑定字符串参数，并添加模糊搜索的通配符
                command.Parameters.Add(":text", OracleDbType.Varchar2).Value = $"%{text}%";
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {                  
                    int id = reader.GetInt32(0);
                    string title = reader.GetString(1);
                    string content = reader.GetString(2);
                    int pid = reader.GetInt32(3);
                    DateTime date = reader.GetDateTime(4);
                    string? username = GetUsernameByUserId(pid);
                    if (username == null)
                    {
                        throw new Exception($"Username not found for userId: {pid}");
                    }
                    posts.Add(new Article(id, title, content, pid, date){ Username = username });
                }
                return posts;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return [];
            }      

        }
        public void UpdatePost(Article post)
        {
            string sql = "UPDATE Articles SET article_title = :Title, article_content = :Content , published_date = SYSDATE WHERE article_id = :ArticleId";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":Title", OracleDbType.Varchar2).Value = post.Title;
            command.Parameters.Add(":Content", OracleDbType.Clob).Value = post.Content;
            command.Parameters.Add(":ArticleId", OracleDbType.Int32).Value = post.ArticleId;
            command.ExecuteNonQuery();

        }
    }
}

