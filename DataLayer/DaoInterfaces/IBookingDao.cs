using DataLayer.Data;

namespace DataLayer.DaoInterfaces
{
    public interface IBookingDao
    {
        BookingEntity Create(int roomNumber, BookingEntity booking);

        BookingEntity Delete(int bookingId);

        BookingEntity Get(int bookingId);

        BookingEntity GetOneBookingByFiscalCode(string fiscalCode);

        IEnumerable<BookingEntity> GetBookingsByClientFiscalCode(string clientFiscalCode);

        IEnumerable<BookingEntity> GetBookingsByTypeOfStay(string typeOfStay);

        void Update(BookingEntity booking);
    }
}
