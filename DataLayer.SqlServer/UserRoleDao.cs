using DataLayer.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.SqlServer
{
    public class UserRoleDao : BaseDao, IUserRoleDao
    {
        public UserRoleDao(IConfiguration configuration, ILogger<UserDao> logger) : base(configuration, logger) { }

        private const string INSERT_USERROLE = "INSERT INTO UsersRoles(UserId, RoleId) VALUES(@userId, @roleId)";
        private const string DELETE_USERROLE = "DELETE FROM UsersRoles WHERE UserId = @userId AND RoleId = @roleId";
        private const string SELECT_BY_USERNAME = "SELECT ro.Name " + 
            "FROM UsersRoles ur " +
            "   JOIN Roles ro ON ur.RoleId = ro.Id " +
            "   JOIN Users us ON ur.UserId = us.Id " +
            "WHERE us.Username = @username";


        public void Create(int userId, int roleId)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(INSERT_USERROLE, conn);
                cmd.Parameters.AddWithValue("@username", userId);
                cmd.Parameters.AddWithValue("@roleName", roleId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception creating role with id = {} for user with id = {}", userId, roleId);
                throw;
            }
        }

        public void Delete(int userId, int roleId)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(DELETE_USERROLE, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@roleId", roleId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception deleting role with id = {} for user with id = {}", userId, roleId);
                throw;
            }
        }

        public List<string> GetAllByUsername(string username)
        {
            var result = new List<string>();
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(SELECT_BY_USERNAME, conn);
                cmd.Parameters.AddWithValue("@username", username);
                using var reader = cmd.ExecuteReader();
                while (reader.Read()) result.Add(reader.GetString(0));
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception reading all roles for user with id = {}", username);
                throw;
            }
        }
    }
}
