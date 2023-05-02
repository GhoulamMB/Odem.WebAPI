using Odem.WebAPI.Models;
using Odem.WebAPI.Models.requests;
using Odem.WebAPI.Models.response;

namespace Odem.WebAPI.Services;

public interface ITransactionService
{
    public Task CreateTransaction(TransactionRequest transaction);
    public Task<List<OdemTransferResponse>> GetTransactions(string userId);
}