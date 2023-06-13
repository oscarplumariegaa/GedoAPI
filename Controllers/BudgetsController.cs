using Gedo.Context;
using Gedo.Controllers;
using Gedo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DocuGen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetsController : ControllerBase
    {
        private readonly BudgetContext _dbContext;
        private readonly ClientContext _clientContext;

        public BudgetsController(BudgetContext dbContext, ClientContext clientContext)
        {
            _dbContext = dbContext;
            _clientContext = clientContext;
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
        [Route("Data")]
        [HttpGet]
        public ActionResult<IEnumerable<Budget>> GetBudgetsData()
        {
            var result = _dbContext.Budgets.ToList();
            var clients = _clientContext.Clients.ToList();
            if (!result.Any())
            {
                return NotFound();
            }
            else
            {
                for (int i = 0; i < result.Count; i++)
                {
                    for(int j = 0; j < clients.Count; j++)
                    {
                        if (result[i].IdClient == clients[j].IdClient)
                        {
                            //var nameForClient = new BudgetInfoClient() { NameClient = clients[j].NameClient };
                            result[i].NameClient = clients[j].NameClient;
                        }
                    }
                }
            }
            return Ok(result);
        }

        public class BudgetInfoClient
        {
            public string NameClient { get; set; }
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
