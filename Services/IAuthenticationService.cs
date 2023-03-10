using Odem.WebAPI.Models;

namespace Odem.WebAPI.Services;

public interface IAuthenticationService
{
    Task<Client> FindUserByEmail(string email);
    Task<bool> Login(string email, string password);
}