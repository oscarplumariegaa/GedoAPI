using Gedo.Context;
using Gedo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gedo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ClientContext _dbContext;

        public ClientsController(ClientContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> Get()
        {
            var result = _dbContext.Clients.ToList();
            if (!result.Any())
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Client>>> Get(int id)
        {
            var result = _dbContext.Clients.FirstOrDefault(x => x.IdClient == id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public void Post([FromBody] Client client)
        {
            _dbContext.Clients.Add(client);
            _dbContext.SaveChanges();
        }
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Client value)
        {
            var client = _dbContext.Clients.FirstOrDefault(x => x.IdClient == id);
            if (client != null)
            {
                client.Address = value.Address;
                client.PhoneNumber = value.PhoneNumber;
                client.CIF = value.CIF;
                client.Email = value.Email;
                client.NameClient = value.NameClient;

                _dbContext.SaveChanges();
            }
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var client = _dbContext.Clients.FirstOrDefault(x => x.IdClient == id);
            if (client != null)
            {
                _dbContext.Clients.Remove(client);
                _dbContext.SaveChanges();
            }
        }
    }
}
