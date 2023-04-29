using Odem.WebAPI.Models;
using Odem.WebAPI.Models.requests;

namespace Odem.WebAPI.Services;

public interface IAdminService
{
    public Task<Admin> FindAdmin(string email);
    public Task<Admin> Login(string email,string password);
    public Task<List<OdemTransfer>> GetTransactions();
    public Task<bool> CreateAdmin(UserRequest admin);
    public Task<List<Client>> GetClients();
    public Task<bool> DeleteClient(string email);
    public Task<bool> UpdateClient(UserRequestAdmin client);
    public Task<Ticket> CreateTicket(string message, string userId, string adminId, bool isClientMessage);
    public Task<Ticket> GetTicket(string ticketId);
    public Task<List<Ticket>> GetTickets();
    public Task<bool> UpdateTicket(string ticketId, string message, string adminId, bool isClientMessage);
    public Task<bool> UpdateTicketStatus(string ticketId, bool close);
}