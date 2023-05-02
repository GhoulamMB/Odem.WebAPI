using Microsoft.AspNetCore.Mvc;
using Odem.WebAPI.Models.response;
using Odem.WebAPI.Services;

namespace Odem.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SupportController : ControllerBase
{
    private readonly ISupportService _supportService;
    
    public SupportController(ISupportService supportService)
    {
        _supportService = supportService;
    }
    
    [HttpGet("ticket")]
    public async Task<TicketResponse> GetTicket(string id)
    {
        return await _supportService.GetTicket(id);
    }
    
    [HttpGet("tickets")]
    public async Task<List<TicketResponse>> GetTickets(string userId)
    {
        return await _supportService.GetTickets(userId);
    }
    
    [HttpPost("createticket")]
    public async Task<TicketResponse> CreateTicket(string message, string userId)
    {
        return await _supportService.CreateTicket(message, userId);
    }
    
    [HttpPut("updateticket")]
    public async Task<bool> UpdateTicket(string ticketId, string message)
    {
        return await _supportService.UpdateTicket(ticketId, message);
    }
}