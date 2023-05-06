using Microsoft.AspNetCore.Mvc;
using Odem.WebAPI.Models;
using Odem.WebAPI.Models.requests;
using Odem.WebAPI.Models.response;
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
        public Task CreateTransaction([FromBody] TransactionRequest transaction)
        {
            return _transactionService.CreateTransaction(transaction);
        }

        [HttpGet]
        public async Task<List<OdemTransferResponse>> GetTransactions(string userId)
        {
           return await _transactionService.GetTransactions(userId);
        }

        [HttpPost("TransferRequest")]
        public async Task<TransferRequest> CreateTransferRequest(string from, string to, double amount, string reason)
        {
            return await _transactionService.CreateTransferRequest(from, to, amount, reason);
        }
        
        [HttpPost("AcceptTransferRequest")]
        public async Task<bool> AcceptTransferRequest(string Id)
        {
            return await _transactionService.AcceptTransferRequest(Id);
        }
        
        [HttpPost("DeclineTransferRequest")]
        public async Task<bool> DeclineTransferRequest(string Id)
        {
            return await _transactionService.DeclineTransferRequest(Id);
        }
    }
}
