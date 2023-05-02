using Odem.WebAPI.Models.requests;
using Odem.WebAPI.Models.response;

namespace Odem.WebAPI.Services;

public interface IAccountService
{
    Task<bool> Register(UserRequest request);
    Task<bool> ChangeInformation(string userId,string email = null, string password = null);
}