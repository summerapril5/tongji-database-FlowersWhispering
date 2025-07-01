using FlowersWhisperingAPI.Plants.Models;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;

namespace FlowersWhisperingAPI.Plants.Mappers
{
    public class PlantLatestMapper(string connectionString)
    {
        private readonly string _connectionString = connectionString;

        public List<Plant> GetLatestPlants()
        {
            var plants = new List<Plant>();
            try
            {
                using (var connection = new OracleConnection(_connectionString))
                {
                    connection.Open();
                    string sql = @"
                            SELECT * 
                            FROM (
                                SELECT PLANTS.PLANT_ID, COMMON_NAME, SCIENTIFIC_NAME, CATEGORY, 
                                    PORTRAYAL, GROWTH_ENVIRONMENT, CARE_CONDITIONS, 
                                    UPDATETIME, PLANTIMAGES.IMAGE_URL
                                FROM PLANTS
                                INNER JOIN PLANTIMAGES ON PLANTS.PLANT_ID = PLANTIMAGES.PLANT_ID
                                ORDER BY PLANTS.PLANT_ID DESC
                            ) WHERE ROWNUM <= 8
                            ";
                    using (var command = new OracleCommand(sql, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // 从数据库中读取数据
                            int plantId = reader.GetInt32(reader.GetOrdinal("PLANT_ID"));
                            string commonName = reader.GetString(reader.GetOrdinal("COMMON_NAME"));
                            string scientificName = reader.GetString(reader.GetOrdinal("SCIENTIFIC_NAME"));
                            string CATEGORY = reader.GetString(reader.GetOrdinal("CATEGORY"));
                            string PORTRAYAL = reader.GetString(reader.GetOrdinal("PORTRAYAL"));
                            string GROWTH_EN = reader.GetString(reader.GetOrdinal("GROWTH_ENVIRONMENT"));
                            string care_con = reader.GetString(reader.GetOrdinal("CARE_CONDITIONS"));
                            DateTime time = reader.GetDateTime(reader.GetOrdinal("UPDATETIME"));
                            string ImageUrl = reader.GetString(reader.GetOrdinal("IMAGE_URL")); 
                            var plant = new Plant(plantId, commonName, scientificName, CATEGORY, PORTRAYAL, GROWTH_EN, care_con, time);
                            plant.ImageUrl = ImageUrl;
                            plants.Add(plant);

                        }
                    }
                }
            }
            catch (OracleException ex)
            {
                // 处理 Oracle 数据库异常
                Console.WriteLine($"OracleException: {ex.Message}");
            }
            catch (Exception ex)
            {
                // 处理其他异常
                Console.WriteLine($"Exception: {ex.Message}");
            }

            return plants;
        }
    }

}