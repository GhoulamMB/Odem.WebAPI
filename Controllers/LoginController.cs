using Microsoft.AspNetCore.Mvc;
using Odem.WebAPI.Models.response;
using Odem.WebAPI.Services;

namespace Odem.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public LoginController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpGet("login")]
    public async Task<ActionResult<ClientResponse>> Login(string email,string password)
    {
        
        try
        {
            return Ok(await _authenticationService.Login(email, password));
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
    
    [HttpGet("loginwithtoken")]
    public async Task<ActionResult> LoginWithToken(string token)
    {
        return Ok(await _authenticationService.LoginWithToken(token));
    }
}