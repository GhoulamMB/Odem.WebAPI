using Microsoft.AspNetCore.Mvc;
using Odem.WebAPI.Models;
using Odem.WebAPI.Services;

namespace Odem.WebAPI.Controllers
{
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
        public Task<bool> Register()
        {
            return Task.FromResult(true);
        }
    }
}
