using DataLayer;
using DataLayer.Data;
using DataLayer.SqlServer;
using Microsoft.AspNetCore.Mvc;

namespace Progetto_settimanale_22_07___26_07.Controllers
{
    public class BookingController : Controller
    {
        private readonly ILogger<RoomController> _logger;
        private readonly DbContext _dbContext;
        public BookingController(DbContext dbContext, ILogger<RoomController> logger)
        {
            _dbContext = dbContext;
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
                    _dbContext.Bookings.Create(roomNumber, bookingEntity);

                    return RedirectToAction("AllRooms", "Room");
                }
                return View(bookingEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception creating booking for room number = {}", roomNumber);
                return StatusCode(500);
            }
            
        }

        [HttpGet("Client/{FiscalCode}/ClientBooking")]
        public IActionResult ClientBooking(string FiscalCode)
        {
            if (string.IsNullOrEmpty(FiscalCode))
            {
                return BadRequest("Fiscal code cannot be null or empty.");
            }
            try
            {
                var booking = _dbContext.Bookings.GetOneBookingByFiscalCode(FiscalCode);

                if (booking == null)
                {
                    return NotFound();
                }
                return View(booking);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception retrieving booking details for fiscal code = {}", FiscalCode);
                return StatusCode(500);
            }
        }
    }
}
