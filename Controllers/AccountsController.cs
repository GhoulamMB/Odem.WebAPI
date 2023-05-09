using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Odem.WebAPI.Services;

namespace Odem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        
        [HttpPut]
        public async Task<ActionResult<bool>> ChangeInformation(string userId, string email = null!, string password = null!)
        {
            if (await _accountService.ChangeInformation(userId, email, password))
            {
                return Ok();
            }

            return NotFound();
        }
    }
}
