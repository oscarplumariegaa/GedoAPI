using Gedo.Context;
using Gedo.Controllers;
using Gedo.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using MimeKit;
using System.Diagnostics;
using System.Net.Mail;
using System.Text;

namespace DocuGen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetsController : ControllerBase
    {
        private readonly BudgetContext _dbContext;
        private readonly ClientContext _clientContext;
        private readonly BillContext _billContext;
        private readonly ConceptContext _conceptContext;

        public BudgetsController(BudgetContext dbContext, ClientContext clientContext, BillContext billContext, ConceptContext conceptContext)
        {
            _dbContext = dbContext;
            _clientContext = clientContext;
            _billContext = billContext;
            _conceptContext = conceptContext;
        }

        //get: api/budgets
        [HttpGet]
        public ActionResult<IEnumerable<Budget>> GetBudgets(int id)
        {
            var result = _dbContext.Budgets.Where(x => x.IdUser == id).ToList();
            if (!result.Any())
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("BudgetsByUser/{id}")]
        public ActionResult<IEnumerable<Budget>> GetBudgetsData(int id)
        {
            var result = _dbContext.Budgets.Where(x => x.IdUser == id).ToList();
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
                            result[i].NameClient = clients[j].NameClient;
                        }
                    }
                }
            }
            return Ok(result);
        }
        [HttpGet("LastBudget/{id}")]
        public ActionResult<Budget> GetIdLastBudget(int id)
        {
            var lastBudget = _dbContext.Budgets.OrderBy(x => x.IdBudget).LastOrDefault(x => x.IdUser == id);
            if (lastBudget != null)
            {
                return Ok(lastBudget);
            }
            else
            {
                return Ok(0);
            }
        }
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
                budget.IdClient = value.IdClient;
                budget.IdBill = value.IdBill;
                budget.NameClient = value.NameClient;
                budget.NameBudget = value.NameBudget;
                budget.Import = value.Import;
                budget.ImportIVA = value.ImportIVA;
                budget.IdUser = value.IdUser;

                _dbContext.SaveChanges();
            }
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var budget = _dbContext.Budgets.FirstOrDefault(x => x.IdBudget == id);
            var bill = _billContext.Bills.FirstOrDefault(x => x.IdBudget == id);
            var concepts = _conceptContext.Concepts.Where(x => x.IdBudget == id).ToList();
            if (budget != null)
            {
                for (int i = 0; i < concepts.Count; i++)
                {
                    _conceptContext.Remove(concepts[i]);
                    _conceptContext.SaveChanges();
                }
                if(bill != null)
                {
                    _billContext.Remove(bill);
                    _billContext.SaveChanges();
                }
                _dbContext.Remove(budget);
                _dbContext.SaveChanges();
            }
        }
        [HttpPost]
        [RequestFormLimits(ValueCountLimit = int.MaxValue, MultipartBodyLengthLimit = long.MaxValue)]
        [DisableRequestSizeLimit]
        [Route("Send_Email")]
        public ActionResult<FilePdf> Post([FromQuery] string to, [FromQuery] string subject, [FromQuery] string from)
        {
            /*string _from = HttpContext.Request.Query["from"].ToString();
            string _subject = HttpContext.Request.Query["subject"].ToString();
            string _to = HttpContext.Request.Query["to"].ToString();*/

            var email = new MailMessage();
            email.From = new System.Net.Mail.MailAddress(from);
            email.To.Add(to);
            email.Subject = subject;

            //var filePath = @"C:/Users/Usuario/Downloads/" + ePdf;
            //var pdfString = new StreamReader(Request.Body).ReadToEnd();
            var pdf = Encoding.UTF8.GetBytes(HttpContext.Request.Form["filename"]);
            //var pdfBinary = Convert.FromBase64String(pdf);
            email.Attachments.Add(new Attachment(new MemoryStream(pdf), "pdf"));

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("", "");
            smtp.EnableSsl = true;
            smtp.Send(email);

            return null;
        }
    }

    public class FilePdf
    {
        public AnyType file { get; set; }
    }

    public class ParamsContact
    {
        public string to { get; set; }
        public string from { get; set; }
        public string subject { get; set; }
    }
}
