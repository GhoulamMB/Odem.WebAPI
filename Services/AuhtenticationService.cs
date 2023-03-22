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
        });
        _mapper = mapperConfiguration.CreateMapper();
    }
    
    public Task<Client?> FindUserByEmail(string email)
    {
        var client = _context.Clients?
            .Include(c => c.Wallet)
            .Include(c => c.Address)
            .Include(c => c.Tickets);

        return Task.FromResult(client?.First(c => c.Email == email));
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
            Wallet = client.Wallet,
            Tickets = _mapper.Map<List<TicketResponse>>(client.Tickets)
        };
        return Task.FromResult(result);
    }

    public async Task<bool> ChangePassword(string email,string password)
    {
        var client = FindUserByEmail(email).Result;
        if (client is not null)
        {
            client.Password = Crypto.EncryptBcrypt(password);
            _context.Clients?.Update(client);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}