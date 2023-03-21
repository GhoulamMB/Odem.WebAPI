using Odem.WebAPI.Models;

namespace Odem.WebAPI.Services;

public class AccountService : IAccountService
{
    private readonly DataContext _context;

    public AccountService()
    {
        _context = new();
    }

    public Task<bool> Register(Client client)
    {
        _context.Clients?.Add(client);
        _context.SaveChanges();
        return Task.FromResult(true);
    }
}