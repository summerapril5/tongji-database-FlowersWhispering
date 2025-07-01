using System.Data;
using FlowersWhisperingAPI.Community.Models;
using Oracle.ManagedDataAccess.Client;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FlowersWhisperingAPI.Community.Mappers
{
   public class CommuniScoreMapper(string connectionString)
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
        public int GetUserScore(int userId)
        {
            int score = 0;
            string sql = "SELECT SUM(points) FROM UserPoints WHERE user_id = :userId";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":userId", OracleDbType.Int32).Value = userId;
            // 使用 ExecuteScalar() 获取聚合查询的结果
            object result = command.ExecuteScalar();

            // 检查结果是否为 null，如果为 null，表示用户没有积分记录
            if (result != DBNull.Value)
            {
                score = Convert.ToInt32(result);
            }
            return score;
        }

        public List<Score> RankScores()
        {
            List<Score> scores = [];
            string sql = @"
                SELECT user_id, SUM(points) AS total_score 
                FROM UserPoints 
                GROUP BY user_id 
                ORDER BY total_score DESC";
            try
            {
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                using var command = new OracleCommand(sql, connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int userId = reader.GetInt32(0);  
                    int totalScore = reader.GetInt32(1);  // 获取总得分 (total_score)
                    string? username = GetUsernameByUserId(userId);
                    if (username == null)
                    {
                        throw new Exception($"Username not found for userId: {userId}");
                    }
                    scores.Add(new Score(userId, totalScore) { Username = username });
                }

                return scores;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<Score>();  // 如果发生异常，返回一个空列表
            }
        }

    }
}

