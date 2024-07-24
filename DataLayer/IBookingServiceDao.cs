using DataLayer.Data;

namespace DataLayer
{
    public interface IBookingServiceDao
    {
        IEnumerable<BookingServiceEntity> GetAll();

        BookingServiceEntity GetById(int id);

        BookingServiceEntity Create(BookingServiceEntity bookingService);

        BookingServiceEntity Update(BookingServiceEntity bookingService);

        BookingServiceEntity Delete(int id);

        IEnumerable<BookingServiceEntity> GetByBookingId(int bookingId);
    }
}
