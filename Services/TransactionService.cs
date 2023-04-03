using Odem.WebAPI.Models;
using Odem.WebAPI.Models.requests;

namespace Odem.WebAPI.Services;

public class TransactionService : ITransactionService
{
    private readonly DataContext _context;

    public TransactionService()
    {
        _context = new();
    }
    public Task CreateTransaction(TransactionRequest transaction)
    {
        var from = new OdemTransfer()
        {
            Amount = transaction.Amount,
            From = _context.Wallets!.Find(transaction.FromWalletId)!,
            To = _context.Wallets.Find(transaction.ToWalletId)!,
            Type = TransactionType.Outgoing
        };
        //Add transaction to database
        _context.OdemTransfers?.Add(from);
        var To = from;
        To.Type = TransactionType.Ongoing;
        //Update wallet balances
        var wallet1 = _context.Wallets.Find(transaction.FromWalletId);
        wallet1!.Balance -= transaction.Amount;
        wallet1.Transactions.Add(from);
        _context.Wallets.Update(wallet1);
        var wallet2 = _context.Wallets.Find(transaction.ToWalletId);
        wallet2!.Balance += transaction.Amount;
        wallet2.Transactions.Add(To);
        _context.Wallets.Update(wallet2);
        _context.SaveChanges();
        
        return Task.CompletedTask;
    }
}