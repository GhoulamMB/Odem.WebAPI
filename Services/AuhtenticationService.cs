using Odem.WebAPI.Models;

namespace Odem.WebAPI.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly DataContext _context;

    public AuthenticationService(DataContext context)
    {
        _context = context;
    }

    public Task<Client> FindUserByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Login(string email, string password)
    {
        throw new NotImplementedException();
    }
}