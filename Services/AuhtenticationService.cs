using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Odem.WebAPI.Models;
using Odem.WebAPI.Models.response;
using Odem.WebAPI.Utils;

namespace Odem.WebAPI.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public AuthenticationService()
    {
        _context = new();
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Ticket, TicketResponse>();
            cfg.CreateMap<Wallet, WalletResponse>();
            cfg.CreateMap<OdemTransfer, OdemTransferResponse>();

        });
        _mapper = mapperConfiguration.CreateMapper();
    }

    public Task<Client?> FindUserByEmail(string email)
    {
        var client = _context.Clients?
            .Include(c => c.Wallet)
            .Include(c=>c.Wallet.Transactions)
            .Include(c => c.Address)
            .Include(c => c.Tickets);

        var result = client?.First(c => c.Email == email);

        result!.Wallet.Transactions = _context.OdemTransfers
            .Include(t => t.From)
            .Where(t=>t.From.Id != result.Wallet.Id)
            .Include(t => t.To)
            .ToList();

        return Task.FromResult(result)!;
    }

    public Task<ClientResponse> Login(string email, string password)
    {
        var client = FindUserByEmail(email).Result;
        
        if (client is null || !Crypto.CompareBcrypt(password, client.Password)) return null!;
        
        var result = new ClientResponse()
        {
            FirstName = client.FirstName,
            LastName = client.LastName,
            Address = client.Address,
            Email = client.Email,
            Password = client.Password,
            Phone = client.Phone,
            Uid = client.Uid,
            Wallet = _mapper.Map<WalletResponse>(client.Wallet),
            Tickets = _mapper.Map<List<TicketResponse>>(client.Tickets)
        };
        _mapper.Map(client.Wallet.Transactions,result.Wallet.Transactions);
        return Task.FromResult(result);
    }

    public Task<bool> ChangePassword(string email,string password)
    {
        var client = FindUserByEmail(email).Result;
        if (client is null) return Task.FromResult(false);
        client.Password = Crypto.EncryptBcrypt(password);
        _context.Clients?.Update(client);
        _context.SaveChanges();
        return Task.FromResult(true);
    }
}
