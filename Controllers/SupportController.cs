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
    public async Task<List<TicketResponse>> GetTickets()
    {
        return await _supportService.GetTickets();
    }
    
    [HttpPost("createticket")]
    public async Task<TicketResponse> CreateTicket(string message, string userId, string adminId)
    {
        return await _supportService.CreateTicket(message, userId, adminId);
    }
}