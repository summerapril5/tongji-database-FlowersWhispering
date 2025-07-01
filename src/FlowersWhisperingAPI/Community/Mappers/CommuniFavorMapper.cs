using System.Collections.Generic;
using System.Data;
using FlowersWhisperingAPI.Community.Models;
using Oracle.ManagedDataAccess.Client;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FlowersWhisperingAPI.Community.Mappers
{
    public class CommuniFavorMapper(string connectionString)
    {
        private readonly string _connectionString = connectionString;

        // 通过 article_id 获取文章实体
        public Article? GetArticleById(int articleId)
        {
            string sql = "SELECT * FROM Articles WHERE article_id = :ArticleId";
            try
            {
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                using var command = new OracleCommand(sql, connection);
                command.Parameters.Add(":ArticleId", OracleDbType.Int32).Value = articleId;

                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    // 读取文章的各个属性
                    int id = reader.GetInt32(reader.GetOrdinal("article_id"));
                    string title = reader.GetString(reader.GetOrdinal("article_title"));
                    string content = reader.GetString(reader.GetOrdinal("article_content"));
                    int userId = reader.GetInt32(reader.GetOrdinal("publisher_id"));
                    DateTime createdDate = reader.GetDateTime(reader.GetOrdinal("published_date"));

                    // 返回 Article 对象
                    return new Article(id, title, content, userId, createdDate);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error retrieving article: {e.Message}");
            }

            return null;  // 如果没有找到文章，返回 null
        }


        public void AddFavor(FavorDTO favordto)
        {
            string sql = @"INSERT INTO FavoritesArticles (user_id, article_id ) VALUES (:UserId, :ArticleId)";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":UserId", OracleDbType.Int32).Value = favordto.UserId;
            command.Parameters.Add(":ArticleId", OracleDbType.Int32).Value = favordto.ArticleId;
            command.ExecuteNonQuery();
        }
        public void DeleteFavor(int favorId)
        {
            string sql = "DELETE FROM FavoritesArticles WHERE favorite_id = :FavorId";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":FavorId", OracleDbType.Int32).Value = favorId;
            command.ExecuteNonQuery();

        }
        public List<Favor> GetAllFavor(int userId)
        {
            List<Favor> favors = [];
            string sql = "SELECT * FROM FavoritesArticles WHERE user_id = :UserId";
            try
            {
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                using var command = new OracleCommand(sql, connection);
                command.Parameters.Add(":UserId", OracleDbType.Int32).Value = userId;
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int FavorId = reader.GetInt32(reader.GetOrdinal("favorite_id"));
                    int UserId = reader.GetInt32(reader.GetOrdinal("user_id"));
                    int articleId = reader.GetInt32(reader.GetOrdinal("article_id"));
                    Article ?article = GetArticleById(articleId);

                    if (article != null)
                    {
                        favors.Add(new Favor(FavorId,UserId,articleId,article.Title,article.Content,article.PublisherId,
                        article.PublishedDate));  // 将文章添加到列表中

                    }
                }
                return favors;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error retrieving favors: {e.Message}");
                return [];  // 如果出错，返回空列表
            }
        }

    }
}

