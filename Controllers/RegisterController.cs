using Microsoft.AspNetCore.Mvc;
using Odem.WebAPI.Models.requests;
using Odem.WebAPI.Services;

namespace Odem.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegisterController : ControllerBase
{
    private readonly IAccountService _accountService;

    public RegisterController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost]
    public async Task<ActionResult> Register(UserRequest clientRequest)
    {
        return await _accountService.Register(clientRequest) ? Ok(true) : NotFound();
    }
}