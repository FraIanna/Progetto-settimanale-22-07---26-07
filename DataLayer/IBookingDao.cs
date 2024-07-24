using DataLayer.Data;

namespace DataLayer
{
    public interface IBookingDao
    {
        BookingEntity Create(BookingEntity booking);

        BookingEntity Delete(int bookingId);

        BookingEntity Get(int bookingId);

        IEnumerable<BookingEntity> GetBookingsByClientFiscalCode(string clientFiscalCode);

        IEnumerable<BookingEntity> GetBookingsByTypeOfStay(string typeOfStay);

        void Update(BookingEntity booking);
    }
}
