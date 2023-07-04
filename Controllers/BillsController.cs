using Gedo.Context;
using Gedo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Gedo.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BillsController : ControllerBase
    {
        private readonly BillContext _dbContext;
        private readonly BudgetContext _budgetContext;

        public BillsController(BillContext dbContext, BudgetContext budgetContext)
        {
            _dbContext = dbContext;
            _budgetContext = budgetContext;
        }

        [HttpGet("BillsByUser/{id}")]
        public ActionResult<IEnumerable<Bill>> GetBills(int id)
        {
            var result = _dbContext.Bills.Where(x => x.IdUser == id).ToList();
            if (!result.Any())
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("BillBudget/{id}")]
        public ActionResult<Bill> GetBillByBudget(int id)
        {
            var b = _dbContext.Bills.OrderBy(x => x.IdBudget).LastOrDefault(x => x.IdBudget == id);
            if (b != null)
            {
                return Ok(b.IdBill);
            }
            else
            {
                return Ok(0);
            }
        }
        [HttpPost]
        public void Post([FromBody] Bill value)
        {
            _dbContext.Bills.Add(value);
            _dbContext.SaveChanges();
            var lastBill = _dbContext.Bills.OrderBy(x => x.IdBill).LastOrDefault();
            var budget = _budgetContext.Budgets.FirstOrDefault(x => x.IdBudget == value.IdBudget);
            if (budget != null)
            {
                budget.IdBill = lastBill.IdBill;
                _budgetContext.SaveChanges();
            }
        }
        [HttpGet("LastBill/{id}")]
        public ActionResult<Bill> GetIdLastBill(int id)
        {
            var lastBill = _dbContext.Bills.OrderBy(x => x.IdBill).LastOrDefault(x => x.IdUser == id);
            if (lastBill != null)
            {
                return Ok(lastBill.IdBill + 1);
            }
            else
            {
                return Ok(1);
            }

        }
        [HttpGet("{id}")]
        public ActionResult<Bill> GetById(int id)
        {
            var bill = _dbContext.Bills.Find(id);
            if (bill == null)
            {
                return NotFound();
            }
            return bill;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var bill = _dbContext.Bills.FirstOrDefault(x => x.IdBill == id);
            if (bill != null)
            {
                var budget = _budgetContext.Budgets.FirstOrDefault(x => x.IdBill == id);
                if (budget != null)
                {
                    budget.IdBill = null;

                    _budgetContext.SaveChanges();
                }
                _dbContext.Bills.Remove(bill);
                _dbContext.SaveChanges();
            }
        }
    }

}
