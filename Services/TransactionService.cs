﻿using Microsoft.EntityFrameworkCore;
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
        var fromClient = _context.Clients?.Include(c => c.Wallet).First(c => c.Email == transaction.FromEmail);
        var toClient = _context.Clients?.Include(c=>c.Wallet).First(c => c.Email == transaction.ToEmail);
        var from = new OdemTransfer()
        {
            Amount = transaction.Amount,
            From = fromClient!.Wallet,
            FromName = $"{fromClient.FirstName} {fromClient.LastName}",
            To = toClient!.Wallet,
            Type = TransactionType.Outgoing
        };
        //Add transaction to database
        _context.OdemTransfers?.Add(from);

        var To = new OdemTransfer()
        {
            Amount = transaction.Amount,
            From = toClient.Wallet,
            ToName = $"{toClient.FirstName} {toClient.LastName}",
            To = toClient.Wallet,
            Type = TransactionType.Ongoing
        };
        
        _context.OdemTransfers?.Add(To);

        //Update wallet balances
        var wallet1 = fromClient.Wallet;
        wallet1.Balance -= transaction.Amount;
        wallet1.Transactions.Add(from);
        _context.Wallets!.Update(wallet1);
        
        var wallet2 = toClient.Wallet;
        wallet2.Balance += transaction.Amount;
        wallet2.Transactions.Add(To);
        _context.Wallets.Update(wallet2);
        
        
        _context.SaveChanges();
        return Task.CompletedTask;
    }
}