using FlowersWhisperingAPI.Plants.Models;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;

namespace FlowersWhisperingAPI.Plants.Mappers
{
    public class PlantFindMapper(string connectionString)
    {
        private readonly string _connectionString = connectionString;

        public int GetPlantId(string name)
        {
            // SQL 查询字符串
            string sql = "SELECT PLANT_ID FROM PLANTS WHERE COMMON_NAME = :PlantName OR SCIENTIFIC_NAME = :PlantName";
            
            try
            {
                // 创建和打开数据库连接
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                // 创建 SQL 命令
                using var command = new OracleCommand(sql, connection);
                // 添加参数
                command.Parameters.Add(":PlantName", OracleDbType.Varchar2).Value = name;
                // 执行命令并读取结果
                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    // 获取 PLANT_ID 列的值
                    return reader.GetInt32(reader.GetOrdinal("PLANT_ID"));
                }
            }
            catch (Exception ex)
            {
                // 捕获并处理异常
                Console.WriteLine($"Error finding plant: {ex.Message}");
                // 报错问题
            }
            
            // 如果没有找到或发生错误，返回 -1
            return -1;
        }
        public Plant GetPlantInfo(string plantName)
        {
            // SQL 查询字符串
            string sql = @"
                            select PLANTS.PLANT_ID, COMMON_NAME, SCIENTIFIC_NAME, CATEGORY, 
                                PORTRAYAL, GROWTH_ENVIRONMENT, CARE_CONDITIONS, 
                                UPDATETIME, PLANTIMAGES.IMAGE_URL
                            from PLANTS 
                            INNER JOIN PLANTIMAGES ON PLANTS.PLANT_ID = PLANTIMAGES.PLANT_ID
                            where COMMON_NAME = :PlantName OR SCIENTIFIC_NAME = :PlantName
                            ";
            try
            {
                // 创建和打开数据库连接
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                
                // 创建 SQL 命令
                using var command = new OracleCommand(sql, connection);
                // 添加参数
                command.Parameters.Add(":PlantName", OracleDbType.Varchar2).Value = plantName;
                // 执行命令并读取结果
                using var reader = command.ExecuteReader();
                if (reader.Read())
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
                    return plant;
                }
            }
            catch (Exception ex)
            {
                // 捕获并处理异常
                Console.WriteLine($"Error finding plant: {ex.Message}");
            }
            
            // 如果没有找到或发生错误，返回 null
            return null!;
            
        }
        
    }

}