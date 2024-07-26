using DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Progetto_settimanale_22_07___26_07.Controllers
{
    [Authorize (Policies.isLogged)]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingApiController : ControllerBase
    {
        private readonly DbContext _dbContext;

        public BookingApiController(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetBookingsByFiscalCode")]
        public ActionResult GetBookingsByFiscalCode(string fiscalCode)
        {
            var bookings = _dbContext.Bookings.GetBookingsByClientFiscalCode(fiscalCode);
            if (bookings == null || !bookings.Any())
            {
                return NotFound(new { message = "Nessuna prenotazione trovata per il codice fiscale fornito." });
            }
            return Ok(bookings);
        }

        [HttpGet("GetTotalFullBoardBookings")]
        public ActionResult GetTotalFullBoardBookings(string typeOfStay)
        {
            var totalFullBoardBookings = _dbContext.Bookings.GetBookingsByTypeOfStay(typeOfStay).Count();
            return Ok(new { total = totalFullBoardBookings });
        }
    }
}
