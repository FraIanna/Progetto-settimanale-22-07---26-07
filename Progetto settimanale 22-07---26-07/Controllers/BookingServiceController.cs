using DataLayer;
using DataLayer.Data;
using DataLayer.SqlServer;
using Microsoft.AspNetCore.Mvc;

namespace Progetto_settimanale_22_07___26_07.Controllers
{
    public class BookingServiceController : Controller
    {
        private readonly DbContext _dbContext;
        private readonly ILogger<BookingController> _logger;

        public BookingServiceController(DbContext dbContext, ILogger<BookingController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet("Booking/{BookingId}/AddService")]
        public IActionResult AddService(int BookingId)
        {
            var services = _dbContext.Services.GetAll().Select(s => new ServiceDto
            {
                ServiceId = s.ServiceId,
                Description = s.Description,
                Price = s.Price,
                Quantity = 1
            }).ToList();

            ViewData["BookingId"] = BookingId;
            return View(services);
        }

        [HttpPost("Booking/SaveSelectedServices")]
        public IActionResult SaveSelectedServices(int bookingId, List<ServiceDto> services)
        {
            var selectedServices = services.Where(s => s.Quantity > 0).ToList();

            foreach (var service in selectedServices)
            {
                var bookingService = new BookingServiceEntity
                {
                    BookingId = bookingId,
                    ServiceId = service.ServiceId,
                    Date = DateTime.Now,
                    Quantity = service.Quantity,
                    Price = service.Price
                };
                _dbContext.BookingsService.Create(bookingService);
            }
            return View(services);
        }
    }
}
