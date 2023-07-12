using Gedo.Context;
using Gedo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

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
        [HttpGet("/{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUser(int id)
        {
            var result = _dbContext.Users.FirstOrDefault(x => x.IdUser == id);
            if (result != null)
            {
                return Ok(result);
                
            }
            return NotFound();
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
            user.Password = hashPassword(user.Password);
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
        [HttpGet("Login/{email}/{password}")]
        public async Task<ActionResult<IEnumerable<User>>> Login(string email, string password)
        {
            var result = _dbContext.Users.Where(u => u.Email == email && u.Password == hashPassword(password)).ToList();
            if (!result.Any())
            {
                return NotFound();
            }
            return Ok(result);
        }
        string hashPassword(string password)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(password);
            var hashedPassword = sha.ComputeHash(asByteArray);

            return Convert.ToBase64String(hashedPassword);
        }
    }
}
