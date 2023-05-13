using Odem.WebAPI.Models;
using Odem.WebAPI.Models.response;

namespace Odem.WebAPI.Services;

public interface IAuthenticationService
{
    Task<Client?> FindUserByEmail(string email);
    Task<ClientResponse> Login(string email, string password);
    Task<ClientResponse?> LoginWithToken(string token);
    Task<bool> ChangePassword(string email,string password);
}