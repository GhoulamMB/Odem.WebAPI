using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Odem.WebAPI.Models;
using Odem.WebAPI.Models.requests;
using Odem.WebAPI.Models.response;

namespace Odem.WebAPI.Services;

public class TransactionService : ITransactionService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly NotificationsService _notificationsService;
    public TransactionService(NotificationsService notificationsService)
    {
        _notificationsService = notificationsService;
        _context = new();
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<OdemTransfer, OdemTransferResponse>();
        });
        _mapper = mapperConfiguration.CreateMapper();
    }
    public async Task<bool> CreateTransaction(TransactionRequest transaction)
    {
        #region transaction
        
        var fromClient = _context.Clients?.Include(c => c.Wallet).First(c => c.Email == transaction.FromEmail);
        var toClient = _context.Clients?.Include(c=>c.Wallet).First(c => c.Email == transaction.ToEmail);
        if (fromClient is null ||
            toClient is null ||
            fromClient.Email == toClient.Email ||
            fromClient.Wallet.Balance <transaction.Amount)
        {
            return false;
        }

        var To = new OdemTransfer
        {
            Amount = transaction.Amount,
            From = fromClient.Wallet,
            PartyOne = $"{fromClient.FirstName} {fromClient.LastName}",
            PartyTwo = $"{toClient.FirstName} {toClient.LastName}",
            To = toClient.Wallet,
            Date = DateTime.Now
        };
        
        _context.OdemTransfers?.Add(To);

        //Update wallet balances
        var wallet1 = fromClient.Wallet;
        wallet1.Balance -= transaction.Amount;
        _context.Wallets!.Update(wallet1);
        
        var wallet2 = toClient.Wallet;
        wallet2.Balance += transaction.Amount;
        _context.Wallets.Update(wallet2);
        
        _context.OdemTransfers?.Add(To);
        
        _context.SaveChanges();
        #endregion

        var message = $"{To.PartyOne} has sent you {transaction.Amount}DZD";
        var playerId = _context.OneSignalIds?.First(o => o.Uid == toClient.Uid).PlayerId;
        await _notificationsService.SendNotification(playerId!, message);
        return true;
    }

    public Task<List<OdemTransferResponse>> GetTransactions(string userId)
    {
        var client = _context.Clients?
            .Include(c => c.Wallet)
            .Include(t => t.Wallet.Transactions)
            .First(c => c.Uid == userId);
        
        client.Wallet.Transactions = _context.OdemTransfers!
            .Include(t => t.From)
            .Where(t=>t.From.Id == client.Wallet.Id || t.To.Id == client.Wallet.Id)
            .Include(t => t.To)
            .ToList();

        var response = _mapper.Map<List<OdemTransferResponse>>(client.Wallet.Transactions);
        return Task.FromResult(response);
    }

    public async Task<TransferRequest> CreateTransferRequest(string from,string to,double amount,string reason)
    {
        if (from == to)
        {
            return null!;
        }
        var clientFrom = _context.Clients?.First(c => c.Email == from);
        var clientTo = _context.Clients?.First(c => c.Email == to);
        if (clientFrom is null
            || clientTo is null) return null!;

        var request = new TransferRequest()
        {
            Amount = amount,
            From = from,
            To = to,
            Checked = false,
            Reason = reason,
            TimeStamp = DateTime.Now
        };
        _context.TransferRequests?.Add(request);
        clientFrom.SentRequests.Add(request);
        clientTo.RecievedRequests.Add(request);
        _context.Clients?.Update(clientFrom);
        _context.Clients?.Update(clientTo);
        await _context.SaveChangesAsync();
        var message = $"{clientFrom.Email} has requested {amount}DZD from you";
        var playerId = _context.OneSignalIds?.First(o => o.Uid == clientTo.Uid).PlayerId;
        await _notificationsService.SendNotification(playerId!, message);
        return request;
    }

    public Task<List<TransferRequest>> GetRequests(string userId)
    {
        var client = _context.Clients?
            .Include(c => c.RecievedRequests)
            .First(c => c.Uid == userId);
        return client?.RecievedRequests != null ? Task.FromResult(client.RecievedRequests) : null!;
    }

    public async Task<bool> AcceptTransferRequest(string Id)
    {
        var request = _context.TransferRequests?.First(r => r.Id == Id);
        if (request is null) return false;
        request.Checked = true;
        _context.TransferRequests?.Update(request);
        await _context.SaveChangesAsync();
        var transaction = new TransactionRequest()
        {
            Amount = request.Amount,
            FromEmail = request.To,
            ToEmail = request.From
        };
        await CreateTransaction(transaction);
        return true;
    }

    public async Task<bool> DeclineTransferRequest(string Id)
    {
        var request = _context.TransferRequests?.First(r => r.Id == Id);
        if (request is null) return false;
        request.Checked = true;
        _context.TransferRequests?.Update(request);
        await _context.SaveChangesAsync();
        var client = _context.Clients?.First(c => c.Email == request.From);
        var message = $"{request.To} has declined your {request.Amount}DZD request";
        var playerId = _context.OneSignalIds?.First(o => o.Uid == client!.Uid).PlayerId;
        await _notificationsService.SendNotification(playerId!, message);
        return true;
    }

    public Task<double> getWalletBalance(string walletId)
    {
        var wallet = _context.Wallets?.First(w => w.Id == walletId);
        return Task.FromResult(wallet!.Balance);
    }
}