using DataLayer;
using DataLayer.Data;
using Microsoft.AspNetCore.Mvc;

namespace Progetto_settimanale_22_07___26_07.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientDao _clientDao;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IClientDao clientDao, ILogger<ClientController> logger)
        {
            _clientDao = clientDao;
            _logger = logger;
        }

        public IActionResult AllClients()
        {
            List<ClientEntity> clients = (List<ClientEntity>)_clientDao.GetAll();
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
                var client = _clientDao.Get(fiscalCode);

                if (client == null)
                {
                    return NotFound();
                }
                return View(client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception retrieving client details for fiscal code = {FiscalCode}", fiscalCode);
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
                _clientDao.Create(client);
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        public IActionResult EditClient(string fiscalCode)
        {
            ClientEntity client = _clientDao.Get(fiscalCode);
            return View();
        }

        [HttpPut("{FiscalCode}")]
        public IActionResult UpdateClient(string fiscalCode, [FromBody] ClientEntity client)
        {
            if (client == null || fiscalCode != client.FiscalCode)
            {
                return BadRequest();
            }

            var updatedClient = _clientDao.Update(fiscalCode, client);
            return Ok(updatedClient);
        }

        public IActionResult DeleteClient(string fiscalCode)
        {
            ClientEntity client = _clientDao.Get(fiscalCode);
            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteClientConfirmed(string fiscalCode)
        {
            _clientDao.Delete(fiscalCode);
            return RedirectToAction(nameof(Index));
        }
    }
}
