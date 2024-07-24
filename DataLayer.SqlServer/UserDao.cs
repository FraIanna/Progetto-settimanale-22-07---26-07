using DataLayer.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace DataLayer.SqlServer
{
    public class UserDao : BaseDao, IUserDao
    {
        public UserDao(IConfiguration configuration, ILogger<UserDao> logger) : base(configuration, logger) { }

        private const string INSERT_USER = @"INSERT INTO Users(Username, Password) OUTPUT INSERTED.Id VALUES(@username, @password)";
        private const string DELETE_USER = "DELETE FROM Users WHERE Id = @userId";
        private const string SELECT_USER_BY_ID = "SELECT Id, Username, Password FROM Users WHERE Id = @userId";
        private const string LOGIN_USER = "SELECT Id, Username, Password FROM Users WHERE Username = @username";
        private const string SELECT_USER_BY_USERNAME = "SELECT Id, Username, Password FROM Users WHERE Username = @username";
        private const string SELECT_ALL_USERS = "SELECT Id, Username, Password FROM Users";


        public UserEntity Create(UserEntity user)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                using var cmd = new SqlCommand(INSERT_USER, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@password", user.Password);
                var Id = (int)cmd.ExecuteScalar();
                return user;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exeptioon creating user");
                throw;
            }
        }

        public UserEntity Delete(int userId)
        {
            try
            {
                var old = Get(userId);
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(DELETE_USER, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.ExecuteNonQuery();
                return old;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception deleting user with id = {}", userId);
                throw;
            }
        }

        public UserEntity Get(int userId)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(SELECT_USER_BY_ID, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                    return new UserEntity
                    {
                        Id = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Password = reader.GetString(2),
                    };
                throw new Exception("User not found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception reading user with id = {}", userId);
                throw;
            }
        }

        public UserEntity Update(int UserId, UserEntity user)
        {
            throw new NotImplementedException();
        }

        public UserEntity? Login(string username)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(LOGIN_USER, conn);
                cmd.Parameters.AddWithValue("@username", username);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                    return new UserEntity
                    {
                        Id = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Password = reader.GetString(2),
                    };
                return null;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception retrieving user {}", username);
                throw;
            }
        }

        public UserEntity GetByUsername(string username)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(SELECT_USER_BY_USERNAME, conn);
                cmd.Parameters.AddWithValue("@username", username);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                    return new UserEntity
                    {
                        Id = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Password = reader.GetString(2),
                    };
                throw new Exception("User not found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception reading user with username = {}", username);
                throw;
            }
        }

        public List<UserEntity> GetAll()
        {
            var result = new List<UserEntity>();
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(SELECT_ALL_USERS, conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    result.Add(new UserEntity
                    {
                        Id = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Password = reader.GetString(2),
                    });
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception reading all users");
                throw;
            }
        }
    }
}
