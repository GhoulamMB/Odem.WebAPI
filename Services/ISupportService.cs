using Odem.WebAPI.Models.response;

namespace Odem.WebAPI.Services;

public interface ISupportService
{
    public Task<TicketResponse> CreateTicket(string message, string userId);
    public Task<TicketResponse> GetTicket(string ticketId);
    public Task<List<TicketResponse>> GetTickets(string userId);
    public Task<bool> UpdateTicket(string message, string ticketId);
}