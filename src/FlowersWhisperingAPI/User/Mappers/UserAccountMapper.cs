using FlowersWhisperingAPI.User.Models;
using Microsoft.AspNetCore.Http;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FlowersWhisperingAPI.User.Mappers
{
    public class UserAccountMapper(string connectionString)
    {
        private readonly string _connectionString = connectionString;

        public UserInfo? GetUserByUsername(string username)
        {
            string sql = "SELECT * FROM users WHERE username = :Username";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":Username", OracleDbType.Varchar2).Value = username;
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                // 从数据读取器中提取用户信息
                int userId = reader.GetInt32(reader.GetOrdinal("user_id"));
                string usernameResult = reader.GetString(reader.GetOrdinal("username"));
                string passwordResult = reader.GetString(reader.GetOrdinal("password"));
                string emailResult = reader.GetString(reader.GetOrdinal("email"));
                string languagePreferenceResult = reader.GetString(reader.GetOrdinal("language_preference"));
                string userStatusResult = reader.GetString(reader.GetOrdinal("user_status"));
                string userRoleResult = reader.GetString(reader.GetOrdinal("user_role"));
                string bioResult = reader.GetString(reader.GetOrdinal("bio"));
                string avatarResult = reader.GetString(reader.GetOrdinal("avatar"));
                string genderResult = reader.GetString(reader.GetOrdinal("gender"));
                UserInfo userInfo = new UserInfo(usernameResult, passwordResult, emailResult, languagePreferenceResult, bioResult, avatarResult, genderResult, userStatusResult, userRoleResult, userId);
                return userInfo;  // 返回 UserInfo 对象
            }
            return null;  // 如果查询结果为空或者出现异常，则返回 null
        }

        public UserInfo? GetUserById(int id)
        {
            string sql = "SELECT * FROM users WHERE user_id = :Id";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":Id", OracleDbType.Int32).Value = id;
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                // 从数据读取器中提取用户信息
                int userId = reader.GetInt32(reader.GetOrdinal("user_id"));
                string usernameResult = reader.GetString(reader.GetOrdinal("username"));
                string passwordResult = reader.GetString(reader.GetOrdinal("password"));
                string emailResult = reader.GetString(reader.GetOrdinal("email"));
                string languagePreferenceResult = reader.GetString(reader.GetOrdinal("language_preference"));
                string userStatusResult = reader.GetString(reader.GetOrdinal("user_status"));
                string userRoleResult = reader.GetString(reader.GetOrdinal("user_role"));
                string bioResult = reader.GetString(reader.GetOrdinal("bio"));
                string avatarResult = reader.GetString(reader.GetOrdinal("avatar"));
                string genderResult = reader.GetString(reader.GetOrdinal("gender"));
                UserInfo userInfo = new UserInfo(usernameResult, passwordResult, emailResult, languagePreferenceResult, bioResult, avatarResult, genderResult, userStatusResult, userRoleResult, userId);
                return userInfo;  // 返回 UserInfo 对象
            }
            return null;  // 如果查询结果为空或者出现异常，则返回 null
        }

        public UserInfo? GetUserByEmail(string email)
        {
            string sql = "SELECT * FROM USERS WHERE email = :Email";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":Email", OracleDbType.Varchar2).Value = email;
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                // 从数据读取器中提取用户信息
                int userId = reader.GetInt32(reader.GetOrdinal("user_id"));
                string usernameResult = reader.GetString(reader.GetOrdinal("username"));
                string passwordResult = reader.GetString(reader.GetOrdinal("password"));
                string emailResult = reader.GetString(reader.GetOrdinal("email"));
                string languagePreferenceResult = reader.GetString(reader.GetOrdinal("language_preference"));
                string userStatusResult = reader.GetString(reader.GetOrdinal("user_status"));
                string userRoleResult = reader.GetString(reader.GetOrdinal("user_role"));
                string bioResult = reader.GetString(reader.GetOrdinal("bio"));
                string avatarResult = reader.GetString(reader.GetOrdinal("avatar"));
                string genderResult = reader.GetString(reader.GetOrdinal("gender"));
                UserInfo userInfo = new UserInfo(usernameResult, passwordResult, emailResult, languagePreferenceResult, bioResult, avatarResult, genderResult, userStatusResult, userRoleResult, userId);
                return userInfo;  // 返回 UserInfo 对象
            }
            return null;  // 如果查询结果为空或者出现异常，则返回 null
        }

        public bool InsertUser(UserInfo userInfo)
        {
            string sql = @"
                INSERT INTO users (username, password, email, user_status, user_role, language_preference,bio,avatar,gender)
                VALUES (:Username, :Password, :Email, :UserStatus, :UserRole, :LanguagePreference, :Bio, :Avatar, :Gender)";
            try
            {
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                using var command = new OracleCommand(sql, connection);
                command.Parameters.Add(":Username", OracleDbType.Varchar2).Value = userInfo.Username;
                command.Parameters.Add(":Password", OracleDbType.Varchar2).Value = userInfo.Password;
                command.Parameters.Add(":Email", OracleDbType.Varchar2).Value = userInfo.Email;
                command.Parameters.Add(":UserStatus", OracleDbType.Varchar2).Value = userInfo.UserStatus;
                command.Parameters.Add(":UserRole", OracleDbType.Varchar2).Value = userInfo.UserRole;
                command.Parameters.Add(":LanguagePreference", OracleDbType.Varchar2).Value = userInfo.LanguagePreference;
                command.Parameters.Add(":Bio", OracleDbType.Varchar2).Value = userInfo.Bio;
                command.Parameters.Add(":Avatar", OracleDbType.Clob).Value = userInfo.Avatar;
                command.Parameters.Add(":Gender", OracleDbType.Varchar2).Value = userInfo.Gender;
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
               Console.WriteLine($"Error inserting user: {ex.Message}");
               return false; // 插入失败
            }           
        }

        public bool UserUpdate(UserDTO userDTO)
        {
            string sql = @"UPDATE users SET username = :Username, password = :Password, email = :Email, 
                    bio = :Bio, avatar = :Avatar, gender = :Gender, 
                    language_preference = :LanguagePreference 
                    WHERE user_id = :UserId";
            try
            {
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                using var command = new OracleCommand(sql, connection);

                // Username
                command.Parameters.Add(":Username", OracleDbType.Varchar2, 50).Value = userDTO.Username ?? (object)DBNull.Value;

                // Password
                command.Parameters.Add(":Password", OracleDbType.Varchar2, 50).Value = userDTO.Password ?? (object)DBNull.Value;

                // Email
                command.Parameters.Add(":Email", OracleDbType.Varchar2, 100).Value = userDTO.Email ?? (object)DBNull.Value;

                // Bio
                command.Parameters.Add(":Bio", OracleDbType.Varchar2, 200).Value = userDTO.Bio != null ? userDTO.Bio.Trim() : (object)DBNull.Value;

                // Avatar
                if (string.IsNullOrEmpty(userDTO.Avatar))
                {
                    command.Parameters.Add(":Avatar", OracleDbType.Clob).Value = DBNull.Value;
                }
                else
                {
                    command.Parameters.Add(":Avatar", OracleDbType.Clob).Value = userDTO.Avatar.Trim();
                }

                // Gender
                command.Parameters.Add(":Gender", OracleDbType.Varchar2, 10).Value = userDTO.Gender != null ? userDTO.Gender.Trim() : (object)DBNull.Value;

                // Language Preference
                command.Parameters.Add(":LanguagePreference", OracleDbType.Varchar2, 10).Value = userDTO.LanguagePreference ?? (object)DBNull.Value;

                // User ID
                command.Parameters.Add(":UserId", OracleDbType.Int32).Value = userDTO.UserId;

                // Execute SQL
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating user: {ex.Message}");
                return false; // Update failed
            }
        }


        public bool IsHaveEmail(string email)
        {
            string sql = "SELECT COUNT(*) FROM users WHERE email = :Email";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":Email", OracleDbType.Varchar2).Value = email;
            var result = command.ExecuteScalar();
            if (result != null && result != DBNull.Value && Convert.ToInt32(result) > 0)
                return true;
            return false; 
        }

        public bool IsHaveUsername(string username)
        {
            string sql = "SELECT COUNT(*) FROM users WHERE username = :Username";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":Username", OracleDbType.Varchar2).Value = username;
            var result = command.ExecuteScalar();
            if (result != null && result != DBNull.Value && Convert.ToInt32(result) > 0)
                return true;
            return false; 
        }
        public DateTime GetRegistrationTimeById(int userId)
        {
            string sql = "SELECT registration_date FROM users WHERE user_id = :UserId";

            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":UserId", OracleDbType.Int32).Value = userId;

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return reader.GetDateTime(0); // 获取注册时间
            }

            throw new Exception("未找到该用户的注册时间");
        }


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
    }
}