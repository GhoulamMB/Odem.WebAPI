using Microsoft.EntityFrameworkCore;
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
        /*var client1 = _context.Clients?.First(c=>c.Email == transaction.FromEmail);
        var client2 = _context.Clients?.First(c=>c.Email == transaction.ToEmail);
        if (client1 == null || client2 == null)
        {
            throw new Exception("Client not found");
        }

        var wallet1 = client1.Wallet;
        var wallet2 = client2.Wallet;
        
        var odemTransfer = new OdemTransfer()
        {
            Amount = transaction.Amount,
            From = wallet1,
            To = wallet2,
            Type = TransactionType.Outgoing
        };

        //Add transaction to database
        _context.OdemTransfers?.Add(odemTransfer);
        odemTransfer.Type = TransactionType.Ongoing;

        //Update wallet balance
        wallet1.Balance -= transaction.Amount;
        wallet1.Transactions.Add(odemTransfer);
        _context.Wallets?.Update(wallet1);
        wallet2.Balance += transaction.Amount;
        wallet2.Transactions.Add(odemTransfer);
        _context.Wallets?.Update(wallet2);
        _context.SaveChanges();*/
        var fromClient = _context.Clients?.Include(c => c.Wallet).First(c => c.Email == transaction.FromEmail);
        var toClient = _context.Clients?.Include(c=>c.Wallet).First(c => c.Email == transaction.ToEmail);
        var from = new OdemTransfer()
        {
            Amount = transaction.Amount,
            From = fromClient!.Wallet,
            To = toClient!.Wallet,
            Type = TransactionType.Outgoing
        };
        //Add transaction to database
        _context.OdemTransfers?.Add(from);
        var To = from;
        To.Type = TransactionType.Ongoing;
        //Update wallet balances
        var wallet1 = fromClient.Wallet;
        wallet1!.Balance -= transaction.Amount;
        wallet1.Transactions.Add(from);
        _context.Wallets!.Update(wallet1);
        var wallet2 = toClient.Wallet;
        wallet2!.Balance += transaction.Amount;
        wallet2.Transactions.Add(To);
        _context.Wallets.Update(wallet2);
        _context.SaveChanges();
        return Task.CompletedTask;
    }
}