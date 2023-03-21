using Odem.WebAPI.Models.requests;

namespace Odem.WebAPI.Services;

public interface IAccountService
{
    Task<bool> Register(UserRequest request);
}