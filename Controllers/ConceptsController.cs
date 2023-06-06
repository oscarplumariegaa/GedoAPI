using Gedo.Context;
using Gedo.Models;
using Microsoft.AspNetCore.Mvc;


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
    }
}
