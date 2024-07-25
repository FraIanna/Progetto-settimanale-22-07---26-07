using DataLayer.DaoInterfaces;
using DataLayer.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace DataLayer.SqlServer
{
    public class ServiceDao : BaseDao, IServiceDao
    {
        public ServiceDao(IConfiguration configuration, ILogger<UserDao> logger) : base(configuration, logger)
        {
        }

        private const string INSERT_SERVICE = @"
            INSERT INTO Services (IdServizio, Descrizione, Prezzo) 
            VALUES (@ServiceId, @Description, @Price)";
        private const string DELETE_SERVICE = "DELETE FROM Service WHERE IdServizio = @ServiceId";
        private const string SELECT_SERVICE_BY_ID = "SELECT * FROM Services WHERE IdServizio = @ServiceId";
        private const string SELECT_ALL_SERVICES = "SELECT * FROM Services";
        private const string UPDATE_SERVICE = @"
            UPDATE Services 
            SET Descrizione = @Description, Prezzo = @Price 
            WHERE IdServizio = @ServiceId";
        public ServiceDto Create(ServiceDto service)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                using var cmd = new SqlCommand(INSERT_SERVICE, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@ServiceId", service.ServiceId);
                cmd.Parameters.AddWithValue("@Description", service.Description);
                cmd.Parameters.AddWithValue("@Price", service.Price);
                cmd.ExecuteNonQuery();
                return service;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception creating service");
                throw;
            }
        }

        public ServiceEntity Delete(int serviceId)
        {
            try
            {
                var old = Get(serviceId);
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(DELETE_SERVICE, conn);
                cmd.Parameters.AddWithValue("@ServiceId", serviceId);
                cmd.ExecuteNonQuery();
                return old;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception deleting service with id = {}", serviceId);
                throw;
            }
        }

        public ServiceEntity Get(int serviceId)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(SELECT_SERVICE_BY_ID, conn);
                cmd.Parameters.AddWithValue("@ServiceId", serviceId);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                    return new ServiceEntity
                    {
                        ServiceId = reader.GetInt32(0),
                        Description = reader.GetString(1),
                        Price = reader.GetDecimal(2)
                    };
                throw new Exception("Service not found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception reading service with id = {}", serviceId);
                throw;
            }
        }

        public IEnumerable<ServiceEntity> GetAll()
        {
            var result = new List<ServiceEntity>();
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(SELECT_ALL_SERVICES, conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new ServiceEntity
                    {
                        ServiceId = reader.GetInt32(0),
                        Description = reader.GetString(1),
                        Price = reader.GetDecimal(2)
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception reading all services");
                throw;
            }
        }

        public void Update(ServiceEntity service)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                using var cmd = new SqlCommand(UPDATE_SERVICE, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@ServiceId", service.ServiceId);
                cmd.Parameters.AddWithValue("@Description", service.Description);
                cmd.Parameters.AddWithValue("@Price", service.Price);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception updating service with id = {}", service.ServiceId);
                throw;
            }
        }
    }
}
