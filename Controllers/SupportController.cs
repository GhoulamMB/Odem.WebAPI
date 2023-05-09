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
    public async Task<ActionResult<TicketResponse>> GetTicket(string id)
    {
        var ticket = await _supportService.GetTicket(id);
        if (ticket == null)
        {
            return NotFound();
        }
        return Ok(ticket);
    }
    
    [HttpGet("tickets")]
    public async Task<ActionResult<List<TicketResponse>>> GetTickets(string userId)
    {
        var tickets = await _supportService.GetTickets(userId);
        if (tickets == null)
        {
            return NotFound();
        }
        return Ok(tickets);
    }
    
    [HttpPost("createticket")]
    public async Task<ActionResult<TicketResponse>> CreateTicket(string message, string userId)
    {
        var ticket = await _supportService.CreateTicket(message, userId);
        if (ticket == null)
        {
            return NotFound();
        }
        return Ok(ticket);
    }
    
    [HttpPut("updateticket")]
    public async Task<ActionResult<TicketResponse>> UpdateTicket(string ticketId, string message)
    {
        var ticket = await _supportService.UpdateTicket(ticketId,message);
        if (ticket == null)
        {
            return NotFound();
        }
        return Ok(ticket);
    }
}