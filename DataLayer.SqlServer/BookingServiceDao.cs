using DataLayer.DaoInterfaces;
using DataLayer.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace DataLayer.SqlServer
{
    public class BookingServiceDao : BaseDao, IBookingServiceDao
    {
        public BookingServiceDao(IConfiguration configuration, ILogger<UserDao> logger) : base(configuration, logger)
        {
        }

        private const string SELECT_ALL_BOOKINGSERVICES = "SELECT IdServizioPrenotazione, IdPrenotazione, IdServizio, Data, Quantita, Prezzo FROM BookingServices";
        private const string SELECT_BOOKINGSERVICE_BY_ID = "SELECT IdServizioPrenotazione, IdPrenotazione, IdServizio, Data, Quantita, Prezzo FROM BookingServices WHERE IdServizioPrenotazione = @id";
        private const string SELECT_BOOKINGSERVICES_BY_BOOKING_ID = "SELECT IdServizioPrenotazione, IdPrenotazione, IdServizio, Data, Quantita, Prezzo FROM BookingServices WHERE IdPrenotazione = @bookingId";
        private const string INSERT_BOOKINGSERVICE = @"INSERT INTO BookingServices(IdPrenotazione, IdServizio, Data, Quantita, Prezzo) 
                                                        OUTPUT INSERTED.IdServizioPrenotazione VALUES(@bookingId, @serviceId, @date, @quantity, @price)";
        private const string UPDATE_BOOKINGSERVICE = @"UPDATE BookingServices SET IdPrenotazione = @bookingId, IdServizio = @serviceId, 
                                                        Data = @date, Quantita = @quantity, Prezzo = @price 
                                                        WHERE IdServizioPrenotazione = @id";
        private const string DELETE_BOOKINGSERVICE = "DELETE FROM BookingServices WHERE IdServizioPrenotazione = @id";

        public BookingServiceEntity Create(BookingServiceEntity bookingService)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                using var cmd = new SqlCommand(INSERT_BOOKINGSERVICE, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@bookingId", bookingService.BookingId);
                cmd.Parameters.AddWithValue("@serviceId", bookingService.ServiceId);
                cmd.Parameters.AddWithValue("@date", bookingService.Date);
                cmd.Parameters.AddWithValue("@quantity", bookingService.Quantity);
                cmd.Parameters.AddWithValue("@price", bookingService.Price);
                var id = (int)cmd.ExecuteScalar();
                bookingService.Id = id;
                return bookingService;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception creating booking service");
                throw;
            }
        }

        public BookingServiceEntity Delete(int id)
        {
            try
            {
                var bookingService = GetById(id);
                using var conn = new SqlConnection(connectionString);
                using var cmd = new SqlCommand(DELETE_BOOKINGSERVICE, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                return bookingService;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception deleting booking service with id = {Id}", id);
                throw;
            }
        }

        public IEnumerable<BookingServiceEntity> GetAll()
        {
            var result = new List<BookingServiceEntity>();
            try
            {
                using var conn = new SqlConnection(connectionString);
                using var cmd = new SqlCommand(SELECT_ALL_BOOKINGSERVICES, conn);
                conn.Open();
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new BookingServiceEntity
                    {
                        Id = reader.GetInt32(0),
                        BookingId = reader.GetInt32(1),
                        ServiceId = reader.GetInt32(2),
                        Date = reader.GetDateTime(3),
                        Quantity = reader.GetInt32(4),
                        Price = reader.GetDecimal(5),
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception retrieving all booking services");
                throw;
            }
            return result;
        }

        public IEnumerable<BookingServiceEntity> GetByBookingId(int bookingId)
        {
            var result = new List<BookingServiceEntity>();
            try
            {
                using var conn = new SqlConnection(connectionString);
                using var cmd = new SqlCommand(SELECT_BOOKINGSERVICES_BY_BOOKING_ID, conn);
                cmd.Parameters.AddWithValue("@bookingId", bookingId);
                conn.Open();
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new BookingServiceEntity
                    {
                        Id = reader.GetInt32(0),
                        BookingId = reader.GetInt32(1),
                        ServiceId = reader.GetInt32(2),
                        Date = reader.GetDateTime(3),
                        Quantity = reader.GetInt32(4),
                        Price = reader.GetDecimal(5),
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception retrieving booking services for booking id = {BookingId}", bookingId);
                throw;
            }
            return result;
        }

        public BookingServiceEntity GetById(int id)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                using var cmd = new SqlCommand(SELECT_BOOKINGSERVICE_BY_ID, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new BookingServiceEntity
                    {
                        Id = reader.GetInt32(0),
                        BookingId = reader.GetInt32(1),
                        ServiceId = reader.GetInt32(2),
                        Date = reader.GetDateTime(3),
                        Quantity = reader.GetInt32(4),
                        Price = reader.GetDecimal(5),
                    };
                }
                throw new Exception("Booking service not found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception retrieving booking service with id = {Id}", id);
                throw;
            }
        }

        public BookingServiceEntity Update(BookingServiceEntity bookingService)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                using var cmd = new SqlCommand(UPDATE_BOOKINGSERVICE, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@id", bookingService.Id);
                cmd.Parameters.AddWithValue("@bookingId", bookingService.BookingId);
                cmd.Parameters.AddWithValue("@serviceId", bookingService.ServiceId);
                cmd.Parameters.AddWithValue("@date", bookingService.Date);
                cmd.Parameters.AddWithValue("@quantity", bookingService.Quantity);
                cmd.Parameters.AddWithValue("@price", bookingService.Price);
                cmd.ExecuteNonQuery();
                return bookingService;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception updating booking service");
                throw;
            }
        }
    }
}
