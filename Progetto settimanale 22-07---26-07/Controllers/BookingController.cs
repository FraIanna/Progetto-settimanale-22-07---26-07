using DataLayer;
using DataLayer.Data;
using DataLayer.SqlServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Progetto_settimanale_22_07___26_07.Models;

namespace Progetto_settimanale_22_07___26_07.Controllers
{
    [Authorize(Policies.isLogged)]
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

        public IActionResult Checkout(int bookingId) 
        {
            var booking = _dbContext.Bookings.Get(bookingId);
            var bookingServices = _dbContext.BookingsService.GetByBookingId(bookingId);

            var services = bookingServices
            .Select(bs => new ServiceDto
            {
                ServiceId = bs.ServiceId,
                Description = _dbContext.Services.Get(bs.ServiceId).Description,
                Price = bs.Price,
                Quantity = bs.Quantity
            })
            .ToList();

            var model = new CheckoutModel
            {
                BookingId = bookingId,
                RoomNumber = booking.RoomNumber,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                Tax = booking.Tax,
                TotalImport = (booking.Tax - booking.Caparra + services.Sum(s => s.Price * s.Quantity)),
                Services = services
            };
            return View(model);
        }
    }
}
