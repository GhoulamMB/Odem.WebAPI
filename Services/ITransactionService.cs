using Odem.WebAPI.Models.requests;

namespace Odem.WebAPI.Services;

public interface ITransactionService
{
    public Task CreateTransaction(TransactionRequest transaction);
}