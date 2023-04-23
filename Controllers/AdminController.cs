using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        
        [HttpPost("createticket")]
        public async Task<Ticket> CreateTicket(string message, string userId, string adminId)
        {
            return await _adminService.CreateTicket(message, userId, adminId);
        }
        
        [HttpGet("tickets")]
        public async Task<List<Ticket>> GetTickets()
        {
            return await _adminService.GetTickets();
        }
        
        [HttpGet("ticket")]
        public async Task<Ticket> GetTicket(string ticketId)
        {
            return await _adminService.GetTicket(ticketId);
        }
        
        [HttpPut("updateticket")]
        public async Task<bool> UpdateTicket(string ticketId, string message, string adminId, bool isClientMessage=true)
        {
            return await _adminService.UpdateTicket(ticketId, message, adminId, isClientMessage);
        }
    }
}
