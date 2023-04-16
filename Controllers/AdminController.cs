using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Odem.WebAPI.Models;
using Odem.WebAPI.Models.requests;
using Odem.WebAPI.Services;

namespace Odem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        
        [HttpGet("login")]
        public async Task<Admin> Login(string email,string password)
        {
            return await _adminService.Login(email, password);
        }
        
        [HttpGet("transactions")]
        public async Task<List<OdemTransfer>> GetTransactions()
        {
            return await _adminService.GetTransactions();
        }
        
        [HttpPost]
        public async Task<bool> CreateAdmin(UserRequest admin)
        {
            return await _adminService.CreateAdmin(admin);
        }
        
        [HttpGet("clients")]
        public async Task<List<Client>> GetClients()
        {
            return await _adminService.GetClients();
        }
        
        [HttpDelete]
        public async Task<bool> DeleteClient(string email)
        {
            return await _adminService.DeleteClient(email);
        }
        
        [HttpPut("update")]
        public async Task<bool> UpdateClient(UserRequest client)
        {
            return await _adminService.UpdateClient(client);
        }
    }
}
