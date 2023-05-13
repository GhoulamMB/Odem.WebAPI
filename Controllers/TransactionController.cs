using Microsoft.AspNetCore.Mvc;
using Odem.WebAPI.Models;
using Odem.WebAPI.Models.requests;
using Odem.WebAPI.Models.response;
using Odem.WebAPI.Services;
using RestSharp;

namespace Odem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateTransaction([FromBody] TransactionRequest transaction)
        {
            var result = await _transactionService.CreateTransaction(transaction);
            if (result is false)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<OdemTransferResponse>>> GetTransactions(string userId)
        {
            var transactions = await _transactionService.GetTransactions(userId);
            if (transactions is null)
            {
                return NotFound();
            }
            return Ok(transactions);
        }

        [HttpPost("TransferRequest")]
        public async Task<ActionResult<TransferRequest>> CreateTransferRequest(string from, string to, double amount, string reason)
        {
            var result = await _transactionService.CreateTransferRequest(from, to, amount, reason);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        
        [HttpPost("AcceptTransferRequest")]
        public async Task<ActionResult<bool>> AcceptTransferRequest(string Id)
        {
            var result = await _transactionService.AcceptTransferRequest(Id);
            if (result is false)
            {
                return NotFound();
            }
            return Ok(result);
        }
        
        [HttpPost("DeclineTransferRequest")]
        public async Task<ActionResult> DeclineTransferRequest(string Id)
        {
            var result = await _transactionService.DeclineTransferRequest(Id);
            if (result is false)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
