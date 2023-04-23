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
        var admin = _context.Admins
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
        var transactions = _context.OdemTransfers.Include(o=>o.From).Include(o=>o.To).ToList();
        return Task.FromResult(transactions);
    }

    public Task<bool> CreateAdmin(UserRequest admin)
    {
        var newAdmin = _mapper.Map<Admin>(admin);
        newAdmin.Password = Crypto.EncryptBcrypt(admin.Password);
        _context.Admins.Add(newAdmin);
        _context.SaveChanges();
        return Task.FromResult(true);
    }

    public Task<List<Client>> GetClients()
    {
        return Task.FromResult(_context.Clients.Include(c=>c.Address).Include(c=>c.Wallet).ToList());
    }

    public Task<bool> DeleteClient(string email)
    {
        var tmp = _context.Clients.Remove(_context.Clients.First(c => c.Email == email));
        _context.SaveChanges();
        if (tmp.Entity is null)
        {
            return Task.FromResult(false);
        }
        return Task.FromResult(true);
    }

    public Task<bool> UpdateClient(UserRequest client)
    {
        var existingClient = _context.Clients.SingleOrDefault(c => c.Email == client.Email);
        if (existingClient == null) return Task.FromResult(false);

        var requestClient = _mapper.Map<Client>(client);
        requestClient.Address = _mapper.Map<Address>(client.Address);
        _context.Clients.Remove(existingClient);
        _context.Clients.Add(requestClient);
        


        _context.SaveChanges();
        return Task.FromResult(true);
    }

    public Task<Ticket> CreateTicket(string message, string userId, string adminId)
    {
        var client = _context.Clients.First(c=>c.Uid == userId);
        var admin = _context.Admins.First(c=>c.Uid == adminId);
        if (client is null) return null!;
        var ticket = new Ticket
        {
            CreatedBy = client,
            Messages = new List<Message>()
            {
                new()
                {
                    Content = message,
                    isClientMessage = true
                }
            },
            HandledBy = admin
        };
        _context.Tickets.Add(ticket);
        _context.SaveChanges();
        return Task.FromResult(ticket);
    }

    public Task<Ticket> GetTicket(string ticketId)
    {
        var ticket = _context.Tickets?
            .Include(t => t.CreatedBy)
            .Include(t => t.HandledBy)
            .Include(m=>m.Messages)
            .First(t => t.Id == ticketId);
        return Task.FromResult(ticket);
    }

    public Task<List<Ticket>> GetTickets()
    {
        var tickets = _context.Tickets?
            .Include(t => t.CreatedBy)
            .Include(t => t.HandledBy)
            .Include(m=>m.Messages)
            .ToList();
        return Task.FromResult(tickets);
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
            isClientMessage = isClientMessage
        });
        _context.SaveChanges();
        return Task.FromResult(true);
    }
}