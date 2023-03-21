using Odem.WebAPI.Models;

namespace Odem.WebAPI.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly DataContext _context;

    public AuthenticationService()
    {
        _context = new();
    }
    
    public Task<Client?> FindUserByEmail(string email)
    {
        return Task.FromResult(_context.Clients?.First(c => c.Email == email));
    }

    public Task<Client?> Login(string email, string password)
    {
        var client = FindUserByEmail(email).Result;
        var isValidPassword = client?.Password == password;
        if (!isValidPassword)
        {
            return null;
        }
        return Task.FromResult(client);
    }

    public async Task<bool> ChangePassword(string email,string password)
    {
        var client = FindUserByEmail(email).Result;
        if (client is not null)
        {
            client.Password = password;
            _context.Clients?.Update(client);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}