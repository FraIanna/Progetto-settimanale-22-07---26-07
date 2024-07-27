using DataLayer.DaoInterfaces;
using DataLayer.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace DataLayer.SqlServer
{
    public class RoomDao : BaseDao, IRoomDao
    {
        public RoomDao(IConfiguration configuration, ILogger<UserDao> logger) : base(configuration, logger)
        {
        }

        private const string INSERT_ROOM = @"
            INSERT INTO Room (NumeroCamera, Descrizione, Tipologia) 
            VALUES (@RoomNumber, @Description, @Type)";
        private const string DELETE_ROOM = "DELETE FROM Room WHERE NumeroCamera = @RoomNumber";
        private const string SELECT_ROOM_BY_ID = "SELECT * FROM Room WHERE NumeroCamera = @RoomNumber";
        private const string SELECT_ALL_ROOMS = "SELECT * FROM Room";
        private const string UPDATE_ROOM = @"
            UPDATE Room 
            SET Descrizione = @Description, Tipologia = @Type 
            WHERE NumeroCamera = @RoomNumber";

        public RoomEntity Create(RoomEntity room)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                using var cmd = new SqlCommand(INSERT_ROOM, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                cmd.Parameters.AddWithValue("@Description", room.Description);
                cmd.Parameters.AddWithValue("@Type", room.Type);
                cmd.ExecuteNonQuery();
                return room;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception creating room");
                throw;
            }
        }

        public RoomEntity Delete(int roomNumber)
        {
            try
            {
                var old = Get(roomNumber);
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(DELETE_ROOM, conn);
                cmd.Parameters.AddWithValue("@RoomNumber", roomNumber);
                cmd.ExecuteNonQuery();
                return old;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception deleting room with number = {}", roomNumber);
                throw;
            }
        }

        public RoomEntity Get(int roomNumber)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(SELECT_ROOM_BY_ID, conn);
                cmd.Parameters.AddWithValue("@RoomNumber", roomNumber);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                    return new RoomEntity
                    {
                        RoomNumber = reader.GetInt32(reader.GetOrdinal("NumeroCamera")),
                        Description = reader.GetString(reader.GetOrdinal("Descrizione")),
                        Type = reader.GetBoolean(reader.GetOrdinal("Tipologia"))
                    };
                throw new Exception("Room not found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception reading room with number = {roomNumber}", roomNumber);
                throw;
            }
        }

        public IEnumerable<RoomEntity> GetAll()
        {
            var result = new List<RoomEntity>();
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(SELECT_ALL_ROOMS, conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new RoomEntity
                    {
                        RoomNumber = reader.GetInt32(reader.GetOrdinal("NumeroCamera")),
                        Description = reader.GetString(reader.GetOrdinal("Descrizione")),
                        Type = reader.GetBoolean(reader.GetOrdinal("Tipologia"))
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception reading all rooms");
                throw;
            }
        }

        public RoomEntity Update(int roomNumber, RoomEntity room)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                using var cmd = new SqlCommand(UPDATE_ROOM, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                cmd.Parameters.AddWithValue("@Description", room.Description);
                cmd.Parameters.AddWithValue("@Type", room.Type);
                cmd.ExecuteNonQuery();
                return room;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception updating room with number = {}", room.RoomNumber);
                throw;
            }
        }
    }
}
