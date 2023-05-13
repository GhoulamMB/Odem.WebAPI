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
    private readonly TokenService _tokenService;

    public AuthenticationService(TokenService tokenService)
    {
        _tokenService = tokenService;
        _context = new();
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Ticket, TicketResponse>()
                .ForMember(dest => dest.Messages,
                    opt => opt.MapFrom(src => src.Messages))
                .ForMember(dest => dest.HandledBy,
                    opt => opt.MapFrom(src => src.HandledBy.FirstName));
                cfg.CreateMap<Wallet, WalletResponse>();
            cfg.CreateMap<OdemTransfer, OdemTransferResponse>();
            cfg.CreateMap<Message, MessageResponse>();

        });
        _mapper = mapperConfiguration.CreateMapper();
    }

    public Task<Client?> FindUserByEmail(string email)
    {
        var client = _context.Clients?
            .Include(c => c.Wallet)
            .Include(c => c.Address)
            .Include(c => c.Tickets)
            .ThenInclude(m => m.HandledBy)
            .Include(msg => msg.Tickets)
            .ThenInclude(msg => msg.Messages)
            .Include(r => r.RecievedRequests)
            .Include(r => r.SentRequests);

        var result = client?.First(c => c.Email == email);

        result!.Wallet.Transactions = _context.OdemTransfers!
            .Include(t => t.From)
            .Where(t=>t.From.Id == result.Wallet.Id || t.To.Id == result.Wallet.Id)
            .Include(t => t.To)
            .ToList();

        return Task.FromResult(result)!;
    }
    
    private Task<Client?> FindUserById(string userId)
    {
        var client = _context.Clients?
            .Include(c => c.Wallet)
            .ThenInclude(w => w.Transactions)
            .Include(c => c.Address)
            .Include(c => c.Tickets)
            .ThenInclude(m => m.HandledBy)
            .Include(msg => msg.Tickets)
            .ThenInclude(msg => msg.Messages);

        var result = client?.First(c => c.Uid == userId);

        result!.Wallet.Transactions = _context.OdemTransfers!
            .Include(t => t.From)
            .Where(t=>t.From.Id == result.Wallet.Id || t.To.Id == result.Wallet.Id)
            .Include(t => t.To)
            .ToList();

        return Task.FromResult(result)!;
    }

    public Task<ClientResponse> Login(string email, string password, string oneSignalId)
    {
        var client = FindUserByEmail(email).Result;

        if (client is null || !Crypto.CompareBcrypt(password, client.Password))
        {
            return null!;
        }
        
        var result = new ClientResponse()
        {
            FirstName = client.FirstName,
            LastName = client.LastName,
            Address = client.Address,
            Email = client.Email,
            Phone = client.Phone,
            Uid = client.Uid,
            Wallet = _mapper.Map<WalletResponse>(client.Wallet),
            Tickets = _mapper.Map<List<TicketResponse>>(client.Tickets),
            RecievedRequests = client.RecievedRequests,
            SentRequests = client.SentRequests
        };
        var token = _tokenService.RegisterToken(result.Uid);
        result.Token = token;
        _mapper.Map(client.Wallet.Transactions,result.Wallet.Transactions);
        var exists = _context.OneSignalIds?.Any(c => c.Uid == client.Uid);
        if (!exists!.Value)
        {
            _context.OneSignalIds?.Add(new OneSignalIds{ PlayerId = oneSignalId, Uid = client.Uid });
            _context.SaveChanges();
        }
        return Task.FromResult(result);
    }

    public Task<ClientResponse> LoginWithToken(string token)
    {
        var userId = _tokenService.RetrieveClientId(token);
        var client = FindUserById(userId.Result).Result;

        var result = new ClientResponse()
        {
            FirstName = client!.FirstName,
            LastName = client.LastName,
            Address = client.Address,
            Email = client.Email,
            Phone = client.Phone,
            Uid = client.Uid,
            Wallet = _mapper.Map<WalletResponse>(client.Wallet),
            Tickets = _mapper.Map<List<TicketResponse>>(client.Tickets)
        };
        result.Token = token;
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
