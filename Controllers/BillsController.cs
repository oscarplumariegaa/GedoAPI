using Gedo.Context;
using Gedo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gedo.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BillsController : ControllerBase
    {
        private readonly BillContext _dbContext;

        public BillsController(BillContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Bill>> GetBills()
        {
            var result = _dbContext.Bills.ToList();
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
                _dbContext.Bills.Remove(bill);
                _dbContext.SaveChanges();
            }
        }
    }

}
