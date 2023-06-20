using Gedo.Context;
using Gedo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Gedo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConceptsController : ControllerBase
    {
        private readonly ConceptContext _dbContext;

        public ConceptsController(ConceptContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Concept>>> Get()
        {
            var result = _dbContext.Concepts.ToList();
            if (!result.Any())
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public void Post([FromBody] Concept concept)
        {
            _dbContext.Concepts.Add(concept);
            _dbContext.SaveChanges();
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                string cmd = $"DELETE FROM Concepts WHERE IdBill={id} OR IdBudget={id}";
                _dbContext.Database.ExecuteSqlRaw(cmd);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
