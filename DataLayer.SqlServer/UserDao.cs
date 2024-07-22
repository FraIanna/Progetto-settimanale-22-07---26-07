using DataLayer.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.Common;
using System.Data.SqlClient;

namespace DataLayer.SqlServer
{
    public class UserDao : IUserDao
    {
        private readonly string connectionString;
        private readonly ILogger<UserDao> logger;

        private const string INSERT_USER = @"INSERT INTO Users(Username, Password) OUTPUT INSERTED.Id VALUES(@username, @password)";

        public UserDao(IConfiguration configuration, ILogger<UserDao> logger) 
        {
            connectionString = configuration.GetConnectionString("MyDb")!;
            this.logger = logger;
        }

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

        public UserEntity Delete(int id)
        {
            throw new NotImplementedException();
        }

        public UserEntity Get(int id)
        {
            throw new NotImplementedException();
        }

        public UserEntity Update(int UserId, UserEntity user)
        {
            throw new NotImplementedException();
        }
    }
}
