using Microsoft.AspNetCore.Mvc;
using Odem.WebAPI.Models.response;
using Odem.WebAPI.Services;

namespace Odem.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly TokenService _tokenService;

    public LoginController(IAuthenticationService authenticationService, TokenService tokenService)
    {
        _authenticationService = authenticationService;
        _tokenService = tokenService;
    }

    [HttpGet("login")]
    public async Task<ClientResponse?> Login(string email,string password)
    {
        return await _authenticationService.Login(email, password);
    }
    
    [HttpGet("loginwithtoken")]
    public async Task<ClientResponse?> LoginWithToken(string token)
    {
        return await _authenticationService.LoginWithToken(token);
    }
}