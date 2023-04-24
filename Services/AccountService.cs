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
}