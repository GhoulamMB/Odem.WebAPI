using Odem.WebAPI.Models;

namespace Odem.WebAPI.Services;

public interface IAuthenticationService
{
    Task<Client?> FindUserByEmail(string email);
    Task<Client?> Login(string email, string password);
    Task<bool> ChangePassword(string email,string password);
}