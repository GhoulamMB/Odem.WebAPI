using Odem.WebAPI.Models;
using Odem.WebAPI.Models.requests;
using Odem.WebAPI.Utils;

namespace Odem.WebAPI.Services;

public class AccountService : IAccountService
{
    private readonly DataContext _context;

    public AccountService()
    {
        _context = new();
    }

    public Task<bool> Register(UserRequest request)
    {
        if(_context.Clients?.Any(c => c.Email == request.Email) ?? false)
            return Task.FromResult(false);
        
        var client = new Client()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            Password = Crypto.EncryptBcrypt(request.Password),
            Address = request.Address
        };
        _context.Clients?.Add(client);
        _context.SaveChanges();
        return Task.FromResult(true);
    }

    public Task<bool> ChangeInformation(string userId,string email = null!, string password = null!)
    {
        var client = _context.Clients?.First(c => c.Uid == userId);
        
        if (email is not null)
        {
            client!.Email = email;
            _context.SaveChanges();
            return Task.FromResult(true);
        }
        if (password is not null)
        {
            client!.Password = Crypto.EncryptBcrypt(password);
            _context.SaveChanges();
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }
}