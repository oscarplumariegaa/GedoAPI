using Gedo.Context;
using Gedo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
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

        [HttpGet("BudgetConcepts/{id}")]
        public ActionResult<IEnumerable<Concept>> GetBudgetConcepts(int id)
        {
            var result = _dbContext.Concepts.Where(c => c.IdBudget == id).ToList();
            if (!result.Any())
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public void Post([FromBody] Concept[] concept)
        {
            foreach (Concept C in concept)
            {
                _dbContext.Concepts.Add(C);
            }
            _dbContext.SaveChanges();
        }
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Concept[] concept)
        {
            var concepts = _dbContext.Concepts.Where(x => x.IdBudget == id).ToList();

            if (concepts.Count == 0)
            {
                foreach (Concept C in concept)
                {
                    _dbContext.Concepts.Add(C);
                    _dbContext.SaveChanges();
                    break;
                }
            }
            else
            {
                for (int i = 0; i < concepts.Count; i++)
                {
                    foreach (Concept C in concept)
                    {
                        if (C.IdConcept == concepts[i].IdConcept)
                        {
                            _dbContext.Entry<Concept>(concepts[i]).CurrentValues.SetValues(C);
                            _dbContext.SaveChanges();
                            break;
                        }
                        else
                        {
                            if (C.IdConcept == 0)
                            {
                                _dbContext.Concepts.Add(C);
                                _dbContext.SaveChanges();
                                break;
                            }
                        }
                    }
                }
            }

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
