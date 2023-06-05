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

        [HttpGet(Name = "GetBills")]
        public ActionResult<IEnumerable<Bill>> GetBudgets()
        {
            if (_dbContext.Bills == null)
            {
                return NotFound();
            }
            return _dbContext.Bills.ToList();
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
