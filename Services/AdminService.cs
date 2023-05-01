using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Odem.WebAPI.Models;
using Odem.WebAPI.Models.requests;
using Odem.WebAPI.Utils;

namespace Odem.WebAPI.Services;

public class AdminService : IAdminService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public AdminService()
    {
        _context = new();
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UserRequest, Admin>();
            cfg.CreateMap<UserRequest, Client>();
            cfg.CreateMap<AddressRequest, Address>();
        });
        _mapper = mapperConfiguration.CreateMapper();
    }

    public Task<Admin> FindAdmin(string email)
    {
        var admin = _context.Admins!
            .Include(a => a.HandledTickets)
            .Include(adr=>adr.Address)
            .First(a => a.Email == email);
        return Task.FromResult(admin);
    }

    public Task<Admin> Login(string email,string password)
    {
        var admin = FindAdmin(email);
        if (admin is null || !Crypto.CompareBcrypt(password, admin.Result.Password)) return null!;
        return admin;
    }

    public Task<List<OdemTransfer>> GetTransactions()
    {
        var transactions = _context.OdemTransfers!
            .Include(o=>o.From)
            .Include(o=>o.To)
            .ToList();
        return Task.FromResult(transactions);
    }

    public Task<bool> CreateAdmin(UserRequest admin)
    {
        var newAdmin = _mapper.Map<Admin>(admin);
        newAdmin.Password = Crypto.EncryptBcrypt(admin.Password);
        _context.Admins!.Add(newAdmin);
        _context.SaveChanges();
        return Task.FromResult(true);
    }

    public Task<List<Client>> GetClients()
    {
        var clients = Task.FromResult(_context!.Clients!
            .Include(c => c.Address)
            .Include(c => c.Wallet)
            .Include(c => c.Wallet.Transactions)
            .Include(c => c.Tickets)
            .ToList());
        return clients;
    }

    public Task<bool> DeleteClient(string email)
    {
        var client = _context.Clients?.First(c => c.Email == email);
        _context.Clients!.Remove(client);
        _context.SaveChanges();
        return Task.FromResult(true);
    }

    public Task<bool> UpdateClient(UserRequestAdmin client)
    {
        var existingClient = _context.Clients?.Include(c=>c.Address).SingleOrDefault(c => c.Email == client.Email);
        if (existingClient == null) return Task.FromResult(false);

        existingClient.FirstName = client.FirstName;
        existingClient.LastName = client.LastName;
        existingClient.Email = client.Email;
        existingClient.Phone = client.Phone;

        _context.Clients!.Update(existingClient);
        
        _context.SaveChanges();
        return Task.FromResult(true);
    }

    public Task<Ticket> CreateTicket(string message, string userEmail, string adminId, bool isClientMessage)
    {
        var client = _context?.Clients!.First(c=>c.Email == userEmail);
        var admin = _context?.Admins!.First(c=>c.Uid == adminId);
        if (client is null) return null!;
        var ticket = new Ticket
        {
            CreatedBy = client,
            Messages = new List<Message>()
            {
                new()
                {
                    Content = message,
                    isClientMessage = isClientMessage
                }
            },
            HandledBy = admin!
        };
        _context?.Tickets!.Add(ticket);
        _context!.SaveChanges();
        return Task.FromResult(ticket);
    }

    public Task<Ticket> GetTicket(string ticketId)
    {
        var ticket = _context.Tickets?
            .Include(t => t.CreatedBy)
            .Include(t => t.HandledBy)
            .Include(m=>m.Messages)
            .First(t => t.Id == ticketId);
        return Task.FromResult(ticket)!;
    }

    public Task<List<Ticket>> GetTickets()
    {
        var tickets = _context.Tickets?
            .Include(t => t.CreatedBy)
            .Include(t => t.HandledBy)
            .Include(m=>m.Messages)
            .ToList();
        return Task.FromResult(tickets)!;
    }

    public Task<bool> UpdateTicket(string ticketId, string message, string adminId, bool isClientMessage)
    {
        var ticket = _context.Tickets?
            .Include(t => t.CreatedBy)
            .Include(t => t.HandledBy)
            .Include(m=>m.Messages)
            .First(t => t.Id == ticketId);
        if (ticket is null) return Task.FromResult(false);
        var admin = _context.Admins?.First(a => a.Uid == adminId);
        ticket.HandledBy = admin!;
        ticket.Messages.Add(new Message
        {
            Content = message,
            isClientMessage = isClientMessage,
            Timestamp = DateTime.Now
        });
        _context.SaveChanges();
        return Task.FromResult(true);
    }

    public Task<bool> UpdateTicketStatus(string ticketId, bool close)
    {
        var ticket = _context.Tickets?
            .Include(t => t.CreatedBy)
            .Include(t => t.HandledBy)
            .Include(m=>m.Messages)
            .First(t => t.Id == ticketId);
        if (ticket is null) return Task.FromResult(false);
        if (close)
        {
            ticket.Status = Status.Closed;
            ticket.CloseDate = DateTime.Now;
        }
        else
        {
            ticket.Status = Status.Open;
            ticket.CloseDate = DateTime.MinValue;
        }
        _context.SaveChanges();
        return Task.FromResult(true);
    }
}