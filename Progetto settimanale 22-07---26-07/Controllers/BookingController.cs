using DataLayer;
using DataLayer.Data;
using DataLayer.SqlServer;
using Microsoft.AspNetCore.Mvc;

namespace Progetto_settimanale_22_07___26_07.Controllers
{
    public class BookingController : Controller
    {
        private readonly ILogger<RoomController> _logger;
        private readonly IBookingDao _bookingDao;
        public BookingController(IBookingDao bookingDao, ILogger<RoomController> logger)
        {
            _bookingDao = bookingDao;
            _logger = logger;
        }

        [HttpGet("Room/{roomNumber}/Booking")]
        public IActionResult Booking(int roomNumber)
        {
            var booking = new BookingEntity
            {
                RoomNumber = roomNumber,
                BookingDate = DateTime.Now
            };
            return View(booking);
        }

        [HttpPost("Room/{roomNumber}/Booking")]
        public IActionResult Booking(int roomNumber, BookingEntity bookingEntity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bookingEntity.RoomNumber = roomNumber;
                    bookingEntity.BookingDate = DateTime.Now;
                    _bookingDao.Create(roomNumber, bookingEntity);

                    return RedirectToAction("AllRooms", "Room");
                }
                return View(bookingEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception creating booking for room number = {RoomNumber}", roomNumber);
                return StatusCode(500);
            }
            
        }
    }
}
