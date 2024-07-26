using DataLayer;
using DataLayer.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Progetto_settimanale_22_07___26_07.Controllers
{
    [Authorize(Policies.isLogged)]
    public class ClientController : Controller
    {
        private readonly DbContext _dbContext;
        private readonly ILogger<ClientController> _logger;

        public ClientController(DbContext dbContext, ILogger<ClientController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult AllClients()
        {
            List<ClientEntity> clients = (List<ClientEntity>)_dbContext.Clients.GetAll();
            return View(clients);
        }

        [HttpGet("{FiscalCode}")]
        public IActionResult ClientDetails(string fiscalCode)
        {
            if (string.IsNullOrEmpty(fiscalCode))
            {
                return BadRequest("Fiscal code cannot be null or empty.");
            }
            try
            {
                var client = _dbContext.Clients.Get(fiscalCode);

                if (client == null)
                {
                    return NotFound();
                }
                return View(client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception retrieving client details for fiscal code = {}", fiscalCode);
                return StatusCode(500);
            }
        }

        public IActionResult CreateClient()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateClient(ClientEntity client)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Clients.Create(client);
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        public IActionResult EditClient(string fiscalCode)
        {
            ClientEntity client = _dbContext.Clients.Get(fiscalCode);
            return View();
        }

        [HttpPut("{FiscalCode}")]
        public IActionResult UpdateClient(string fiscalCode, [FromBody] ClientEntity client)
        {
            if (client == null || fiscalCode != client.FiscalCode)
            {
                return BadRequest();
            }

            var updatedClient = _dbContext.Clients.Update(fiscalCode, client);
            return Ok(updatedClient);
        }

        public IActionResult DeleteClient(string fiscalCode)
        {
            ClientEntity client = _dbContext.Clients.Get(fiscalCode);
            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteClientConfirmed(string fiscalCode)
        {
            _dbContext.Clients.Delete(fiscalCode);
            return RedirectToAction(nameof(Index));
        }
    }
}
