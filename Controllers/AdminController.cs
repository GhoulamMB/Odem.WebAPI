using Microsoft.AspNetCore.Mvc;
using Odem.WebAPI.Models;
using Odem.WebAPI.Models.requests;
using Odem.WebAPI.Models.response;
using Odem.WebAPI.Services;

namespace Odem.WebAPI.Controllers;

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
    public async Task<ActionResult<Admin>> Login(string email,string password)
    {
        var admin = await _adminService.Login(email, password);
        if (admin == null)
        {
            return NotFound();
        }
        return Ok(admin);
    }
        
    [HttpGet("transactions")]
    public async Task<ActionResult<OdemTransfer>> GetTransactions()
    {
        var transactions = await _adminService.GetTransactions();
        if (transactions == null)
        {
            return NotFound();
        }
        return Ok(transactions);
    }
        
    [HttpPost]
    public async Task<ActionResult> CreateAdmin(UserRequest admin)
    {
        return await _adminService.CreateAdmin(admin) ? Ok(true) : NotFound();
    }
        
    [HttpGet("clients")]
    public async Task<ActionResult<List<Client>>> GetClients()
    {
        var clients = await _adminService.GetClients();
        if (clients is null)
        {
            return NotFound();
        }
        return Ok(clients);
    }
        
    [HttpDelete]
    public async Task<ActionResult> DeleteClient(string email)
    {
        return await _adminService.DeleteClient(email) ? Ok(true) : NotFound();
    }
        
    [HttpPut("update")]
    public async Task<ActionResult> UpdateClient(UserRequestAdmin client)
    {
        return await _adminService.UpdateClient(client) ? Ok(true) : NotFound();
    }
        
    [HttpPost("createticket")]
    public async Task<ActionResult<Ticket>> CreateTicket(string message, string userEmail, string adminId, bool isClientMessage=false)
    {
        var ticket = await _adminService.CreateTicket(message, userEmail, adminId, isClientMessage);
        if (ticket == null)
        {
            return NotFound();
        }
        return Ok(ticket);
    }
        
    [HttpGet("tickets")]
    public async Task<ActionResult<List<Ticket>>> GetTickets()
    {
        var tickets = await _adminService.GetTickets();
        if (tickets == null)
        {
            return NotFound();
        }
        return Ok(tickets);
    }
        
    [HttpGet("ticket")]
    public async Task<ActionResult<Ticket>> GetTicket(string ticketId)
    {
        var ticket = await _adminService.GetTicket(ticketId);
        if (ticket == null)
        {
            return NotFound();
        }
        return Ok(ticket);
    }
        
    [HttpPut("updateticket")]
    public async Task<ActionResult> UpdateTicket(string ticketId, string message, string adminId, bool isClientMessage=true)
    {
        return await _adminService.UpdateTicket(ticketId, message, adminId, isClientMessage) ? Ok(true) : NotFound();
    }
        
    [HttpPut("updateticketstatus")]
    public async Task<ActionResult> UpdateTicketStatus(string ticketId, bool close)
    {
        return await _adminService.UpdateTicketStatus(ticketId, close) ? Ok(true) : NotFound();
    }
}