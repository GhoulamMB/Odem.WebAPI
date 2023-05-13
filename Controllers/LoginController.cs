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
    public async Task<ActionResult<ClientResponse>> Login(string email,string password, string oneSignalId)
    {
        try
        {
            return Ok(await _authenticationService.Login(email, password,oneSignalId));
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
    
    [HttpGet("loginwithtoken")]
    public async Task<ActionResult<ClientResponse>> LoginWithToken(string token)
    {
        try
        {
            var response = await _authenticationService.LoginWithToken(token);
            return Ok(response);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
}