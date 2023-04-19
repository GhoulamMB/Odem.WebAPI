using Odem.WebAPI.Models;
using Odem.WebAPI.Models.response;

namespace Odem.WebAPI.Services;

public interface ISupportService
{
    public Task<TicketResponse> CreateTicket(string message, string userId, string adminId);
    public Task<TicketResponse> GetTicket(string ticketId);
    public Task<List<TicketResponse>> GetTickets();
}