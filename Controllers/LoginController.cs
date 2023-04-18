using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Odem.WebAPI.Models.response;
using Odem.WebAPI.Services;

namespace Odem.WebAPI.Controllers
{
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
        public async Task<ClientResponse?> Login(string email,string password)
        {
            return await _authenticationService.Login(email, password);
        }
    }
}
