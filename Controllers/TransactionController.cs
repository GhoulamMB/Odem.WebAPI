using Microsoft.AspNetCore.Mvc;
using Odem.WebAPI.Models;
using Odem.WebAPI.Models.requests;
using Odem.WebAPI.Services;

namespace Odem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionsController()
        {
            _transactionService = new TransactionService();
        }
        
        [HttpPost]
        public IActionResult CreateTransaction([FromBody] TransactionRequest transaction)
        {
            _transactionService.CreateTransaction(transaction);
            return Ok();
        }
    }
}
