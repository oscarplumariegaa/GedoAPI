using Gedo.Context;
using Gedo.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocuGen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetsController : ControllerBase
    {
        private readonly BudgetContext _dbContext;

        public BudgetsController(BudgetContext dbContext)
        {
            _dbContext = dbContext;
        }

        //get: api/budgets
        [HttpGet]
        public ActionResult<IEnumerable<Budget>> GetBudgets()
        {
            var result = _dbContext.Budgets.ToList();
            if (!result.Any())
            {
                return NotFound();
            }
            return Ok(result);
        }

        //get: api/budget/id
        [HttpGet("{id}")]
        public ActionResult<Budget> GetById(int id)
        {
            var budget = _dbContext.Budgets.FirstOrDefault(x => x.IdBudget == id);
            if (budget != null)
            {
                return Ok(budget);
            }
            return NotFound();
        }
        [HttpPost]
        public void Post([FromBody] Budget value)
        {
            _dbContext.Budgets.Add(value);
            _dbContext.SaveChanges();
        }
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Budget value)
        {
            var budget = _dbContext.Budgets.FirstOrDefault(x => x.IdBudget == id);
            if (budget != null)
            {
                _dbContext.Entry<Budget>(budget).CurrentValues.SetValues(value);
                _dbContext.SaveChanges();
            }
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var budget = _dbContext.Budgets.FirstOrDefault(x => x.IdBudget == id);
            if (budget != null)
            {
                _dbContext.Remove(budget);
                _dbContext.SaveChanges();
            }
        }
    }

}
