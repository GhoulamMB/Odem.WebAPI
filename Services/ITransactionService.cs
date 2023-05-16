using Odem.WebAPI.Models;
using Odem.WebAPI.Models.requests;
using Odem.WebAPI.Models.response;

namespace Odem.WebAPI.Services;

public interface ITransactionService
{
    public Task<bool> CreateTransaction(TransactionRequest transaction);
    public Task<List<OdemTransferResponse>> GetTransactions(string userId);

    public Task<TransferRequest> CreateTransferRequest(string from,string to,double amount,string reason);

    public Task<List<TransferRequest>> GetRequests(string userId);
    public Task<bool> AcceptTransferRequest(string Id);
    public Task<bool> DeclineTransferRequest(string Id);
    public Task<double> getWalletBalance(string walletId);
}