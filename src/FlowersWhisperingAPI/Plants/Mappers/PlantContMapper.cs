using FlowersWhisperingAPI.Plants.Models;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;

namespace FlowersWhisperingAPI.Plants.Mappers
{
    public class PlantContMapper(string connectionString)
    {
        public List<PlantWithReview> GetContPlants(int userId)
        {
            List<PlantWithReview> plants = new List<PlantWithReview>();
            try
            {
                string sql = @"
                            SELECT REVIEW_ID, PLANTS.PLANT_ID, COMMON_NAME, SCIENTIFIC_NAME, 
                                   MODIFIED_CONTENT, REVIEW_STATUS
                            FROM REVIEWS
                            INNER JOIN PLANTS ON REVIEWS.PLANT_ID = PLANTS.PLANT_ID
                            WHERE SUBMITTER_ID = :userId
                            ";
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add(new OracleParameter("userId", userId));
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int plantId = reader.GetInt32(reader.GetOrdinal("PLANT_ID"));
                                int reviewId = reader.GetInt32(reader.GetOrdinal("REVIEW_ID"));
                                string commonName = reader.GetString(reader.GetOrdinal("COMMON_NAME"));
                                string scientificName = reader.GetString(reader.GetOrdinal("SCIENTIFIC_NAME"));
                               
                                string modifiedContent = reader.GetString(reader.GetOrdinal("MODIFIED_CONTENT"));
                                string reviewStatus = reader.GetString(reader.GetOrdinal("REVIEW_STATUS"));
                                
                                var plant = new PlantWithReview(plantId, reviewId, commonName, scientificName, modifiedContent, reviewStatus);
                                plants.Add(plant);
                            }
                        }
                    }
                    // 如果成功，返回 plants
                    return plants;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving Contribute plants: {ex.Message}");
            }
            // 如果发生错误，返回 null
            return null!;
        }


        public bool AddContPlant(int userId, string plantName, string reviewContent)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    // 查询 plantId
                    string querySql = @"
                        SELECT PLANT_ID 
                        FROM PLANTS 
                        WHERE COMMON_NAME = :plantName OR SCIENTIFIC_NAME = :plantName";
                    
                    int? plantId;
                    using (OracleCommand queryCommand = new OracleCommand(querySql, connection))
                    {
                        queryCommand.Parameters.Add(new OracleParameter("plantName", plantName));
                        using (OracleDataReader reader = queryCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                plantId = reader.GetInt32(reader.GetOrdinal("PLANT_ID"));
                            }
                            else
                            {
                                plantId = null!;
                            }
                        }
                    }

                    // 插入评论记录
                    string insertSql = @"
                        INSERT INTO REVIEWS (PLANT_ID, SUBMITTER_ID, MODIFIED_CONTENT)
                        VALUES (:plantId, :userId, :reviewContent)";
                    
                    using (OracleCommand insertCommand = new OracleCommand(insertSql, connection))
                    {
                        insertCommand.Parameters.Add(new OracleParameter("plantId", plantId));
                        insertCommand.Parameters.Add(new OracleParameter("userId", userId));
                        insertCommand.Parameters.Add(new OracleParameter("reviewContent", reviewContent));
                        insertCommand.ExecuteNonQuery();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding contribute plant: {ex.Message}");
            }
            return false;
        }
    }

}