using DataLayer;
using DataLayer.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Progetto_settimanale_22_07___26_07.Controllers
{
    [Authorize(Policies.isLogged)]
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

        public IActionResult EditRoom(int roomNumber)
        {
            var room = _dbContext.Rooms.Get(roomNumber);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        [HttpPost]
        public IActionResult EditRoom(RoomEntity room) 
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Rooms.Update(room.RoomNumber, room);
                    return RedirectToAction(nameof(AllRooms));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Exception updating room with room number = {}", room.RoomNumber);
                    return StatusCode(500);
                }
            }
            return View(room);
        }
    }
}
