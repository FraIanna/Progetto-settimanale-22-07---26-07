using DataLayer.DaoInterfaces;
using DataLayer.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace DataLayer.SqlServer
{
    public class BookingDao : BaseDao, IBookingDao
    {
        public BookingDao(IConfiguration configuration, ILogger<UserDao> logger) : base(configuration, logger)
        {
        }

        private const string INSERT_BOOKING = @"
            INSERT INTO Booking (CodiceFiscaleCliente, NumeroCamera, DataPrenotazione, NumeroProgressivoAnno, Anno, DataInizio, DataFine, Caparra, Tariffa, TipologiaSoggiorno) 
            OUTPUT INSERTED.IdPrenotazione 
            VALUES (@ClientFiscalCode, @RoomNumber, @BookingDate, @NumeroProgressivoAnno, @Year, @StartDate, @EndDate, @Caparra, @Tax, @TypeOfStay)";
        private const string DELETE_BOOKING = "DELETE FROM Booking WHERE IdPrenotazione = @BookingId";
        private const string SELECT_BOOKING_BY_ID = "SELECT * FROM Booking WHERE IdPrenotazione = @BookingId";
        private const string SELECT_BOOKINGS_BY_CLIENT = "SELECT * FROM Booking WHERE CodiceFiscaleCliente = @ClientFiscalCode";
        private const string SELECT_BOOKINGS_BY_TYPE = "SELECT * FROM Booking WHERE TipologiaSoggiorno = @TypeOfStay";
        private const string UPDATE_BOOKING = @"
            UPDATE Booking 
            SET CodiceFiscaleCliente = @ClientFiscalCode, NumeroCamera = @RoomNumber, DataPrenotazione = @BookingDate, 
                NumeroProgressivoAnno = @NumeroProgressivoAnno, Anno = @Year, DataInizio = @StartDate, DataFine = @EndDate, 
                Caparra = @Caparra, Tariffa = @Tax, TipologiaSoggiorno = @TypeOfStay 
            WHERE IdPrenotazione = @BookingId";

        public BookingEntity Create(int roomNumber, BookingEntity booking)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                using var cmd = new SqlCommand(INSERT_BOOKING, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@ClientFiscalCode", booking.ClientFiscalCode);
                cmd.Parameters.AddWithValue("@RoomNumber", booking.RoomNumber);
                cmd.Parameters.AddWithValue("@BookingDate", booking.BookingDate);
                cmd.Parameters.AddWithValue("@NumeroProgressivoAnno", booking.NumeroProgressivoAnno);
                cmd.Parameters.AddWithValue("@Year", booking.Year);
                cmd.Parameters.AddWithValue("@StartDate", booking.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", booking.EndDate);
                cmd.Parameters.AddWithValue("@Caparra", booking.Caparra);
                cmd.Parameters.AddWithValue("@Tax", booking.Tax);
                cmd.Parameters.AddWithValue("@TypeOfStay", booking.TypeOfStay);
                booking.BookingId = (int)cmd.ExecuteScalar();
                return booking;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception creating booking");
                throw;
            }
        }

        public BookingEntity Delete(int bookingId)
        {
            try
            {
                var old = Get(bookingId);
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(DELETE_BOOKING, conn);
                cmd.Parameters.AddWithValue("@BookingId", bookingId);
                cmd.ExecuteNonQuery();
                return old;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception deleting booking with id = {}", bookingId);
                throw;
            }
        }

        public BookingEntity Get(int bookingId)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(SELECT_BOOKING_BY_ID, conn);
                cmd.Parameters.AddWithValue("@BookingId", bookingId);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                    return new BookingEntity
                    {
                        BookingId = reader.GetInt32(0),
                        ClientFiscalCode = reader.GetString(1),
                        RoomNumber = reader.GetInt32(2),
                        BookingDate = reader.GetDateTime(3),
                        NumeroProgressivoAnno = reader.GetInt32(4),
                        Year = reader.GetInt32(5),
                        StartDate = reader.GetDateTime(6),
                        EndDate = reader.GetDateTime(7),
                        Caparra = reader.GetDecimal(8),
                        Tax = reader.GetDecimal(9),
                        TypeOfStay = reader.GetString(10)
                    };
                throw new Exception("Booking not found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception reading booking with id = {}", bookingId);
                throw;
            }
        }

        public IEnumerable<BookingEntity> GetBookingsByClientFiscalCode(string clientFiscalCode)
        {
            var result = new List<BookingEntity>();
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(SELECT_BOOKINGS_BY_CLIENT, conn);
                cmd.Parameters.AddWithValue("@ClientFiscalCode", clientFiscalCode);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new BookingEntity
                    {
                        BookingId = reader.GetInt32(0),
                        ClientFiscalCode = reader.GetString(1),
                        RoomNumber = reader.GetInt32(2),
                        BookingDate = reader.GetDateTime(3),
                        NumeroProgressivoAnno = reader.GetInt32(4),
                        Year = reader.GetInt32(5),
                        StartDate = reader.GetDateTime(6),
                        EndDate = reader.GetDateTime(7),
                        Caparra = reader.GetDecimal(8),
                        Tax = reader.GetDecimal(9),
                        TypeOfStay = reader.GetString(10)
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception reading bookings by client fiscal code");
                throw;
            }
        }
        
        public BookingEntity GetOneBookingByFiscalCode(string clientFiscalCode)
        {
            var result = new BookingEntity();
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(SELECT_BOOKINGS_BY_CLIENT, conn);
                cmd.Parameters.AddWithValue("@ClientFiscalCode", clientFiscalCode);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    return new BookingEntity
                    {
                        BookingId = reader.GetInt32(0),
                        ClientFiscalCode = reader.GetString(1),
                        RoomNumber = reader.GetInt32(2),
                        BookingDate = reader.GetDateTime(3),
                        NumeroProgressivoAnno = reader.GetInt32(4),
                        Year = reader.GetInt32(5),
                        StartDate = reader.GetDateTime(6),
                        EndDate = reader.GetDateTime(7),
                        Caparra = reader.GetDecimal(8),
                        Tax = reader.GetDecimal(9),
                        TypeOfStay = reader.GetString(10)
                    };
                }
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception reading bookings by client fiscal code");
                throw;
            }
        }

        public IEnumerable<BookingEntity> GetBookingsByTypeOfStay(string typeOfStay)
        {
            var result = new List<BookingEntity>();
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(SELECT_BOOKINGS_BY_TYPE, conn);
                cmd.Parameters.AddWithValue("@TypeOfStay", typeOfStay);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new BookingEntity
                    {
                        BookingId = reader.GetInt32(0),
                        ClientFiscalCode = reader.GetString(1),
                        RoomNumber = reader.GetInt32(2),
                        BookingDate = reader.GetDateTime(3),
                        NumeroProgressivoAnno = reader.GetInt32(4),
                        Year = reader.GetInt32(5),
                        StartDate = reader.GetDateTime(6),
                        EndDate = reader.GetDateTime(7),
                        Caparra = reader.GetDecimal(8),
                        Tax = reader.GetDecimal(9),
                        TypeOfStay = reader.GetString(10)
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception reading bookings by type of stay");
                throw;
            }
        }

        public void Update(BookingEntity booking)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                using var cmd = new SqlCommand(UPDATE_BOOKING, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@BookingId", booking.BookingId);
                cmd.Parameters.AddWithValue("@ClientFiscalCode", booking.ClientFiscalCode);
                cmd.Parameters.AddWithValue("@RoomNumber", booking.RoomNumber);
                cmd.Parameters.AddWithValue("@BookingDate", booking.BookingDate);
                cmd.Parameters.AddWithValue("@NumeroProgressivoAnno", booking.NumeroProgressivoAnno);
                cmd.Parameters.AddWithValue("@Year", booking.Year);
                cmd.Parameters.AddWithValue("@StartDate", booking.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", booking.EndDate);
                cmd.Parameters.AddWithValue("@Caparra", booking.Caparra);
                cmd.Parameters.AddWithValue("@Tax", booking.Tax);
                cmd.Parameters.AddWithValue("@TypeOfStay", booking.TypeOfStay);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception updating booking with id = {}", booking.BookingId);
                throw;
            }
        }
    }
}
 