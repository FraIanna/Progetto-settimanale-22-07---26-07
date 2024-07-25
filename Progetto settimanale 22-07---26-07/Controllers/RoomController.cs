using DataLayer;
using DataLayer.Data;
using Microsoft.AspNetCore.Mvc;

namespace Progetto_settimanale_22_07___26_07.Controllers
{
    public class RoomController : Controller
    {
        private readonly ILogger<RoomController> _logger;
        private readonly DbContext _dbContext;
        public RoomController(DbContext dbContext, ILogger<RoomController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult AllRooms()
        {
            List<RoomEntity> rooms =  (List<RoomEntity>)_dbContext.Rooms.GetAll();
            return View(rooms);
        }

        public IActionResult DetailRooms(int roomNumber) 
        {
            try
            {
                var room = _dbContext.Rooms.Get(roomNumber);
                if (roomNumber == null)
                {
                    return NotFound();
                }
                return View(room);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Exception retrieving room details for room number = {RoomNumber}", roomNumber);
                return StatusCode(500);
            }
        }
    }
}
