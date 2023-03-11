using Odem.WebAPI.Models;

namespace Odem.WebAPI.Services;

public interface IAccountService
{
    Task<bool> Register(Client client);
}