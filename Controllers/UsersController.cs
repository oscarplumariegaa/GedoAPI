using Gedo.Context;
using Gedo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gedo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _dbContext;

        public UsersController(UserContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var result = _dbContext.Users.ToList();
            if (!result.Any())
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public void Post([FromBody] User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
    }
}
