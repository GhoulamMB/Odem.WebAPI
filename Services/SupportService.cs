using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Odem.WebAPI.Models;
using Odem.WebAPI.Models.response;

namespace Odem.WebAPI.Services;

public class SupportService : ISupportService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public SupportService()
    {
        _context = new();
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Ticket, TicketResponse>()
                .ForMember(dest => 
                        dest.Messages,
                    opt =>
                        opt.MapFrom(src => src.Messages))
                .ForMember(dest =>
                        dest.HandledBy,
                    opt =>
                        opt.MapFrom(src =>
                            src.HandledBy.FirstName));
            cfg.CreateMap<Message, MessageResponse>();
        });
        _mapper = mapperConfiguration.CreateMapper();
    }
    public Task<TicketResponse> CreateTicket(string message, string userId)
    {
        var client = _context.Clients?.First(c=>c.Uid == userId);
        var admin = _context.Admins?.First();
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
            HandledBy = admin!,
            CreationDate = DateTime.Now
        };
        _context.Tickets?.Add(ticket);
        _context.SaveChanges();
        var response = _mapper.Map(ticket, new TicketResponse());
        return Task.FromResult(response);
    }

    public Task<TicketResponse> GetTicket(string ticketId)
    {
        var ticket = _context.Tickets?
            .Include(t => t.CreatedBy)
            .Include(t => t.HandledBy)
            .Include(m=>m.Messages)
            .First(t => t.Id == ticketId);
        var response = _mapper.Map(ticket, new TicketResponse());
        return Task.FromResult(response);
    }

    public Task<List<TicketResponse>> GetTickets(string userId)
    {
        var request = _context.Tickets?
            .Include(t => t.HandledBy)
            .Include(t => t.CreatedBy)
            .Include(t => t.Messages)
            .Where(t => t.CreatedBy.Uid == userId)
            .ToList();
        
        var result = _mapper.Map<List<TicketResponse>>(request);
        return Task.FromResult(result);
    }

    public Task<MessageResponse> UpdateTicket(string ticketId, string message)
    {
        var ticket = _context.Tickets?.First(t => t.Id == ticketId);
        var messageReq = new Message
        {
            Content = message,
            isClientMessage = true,
            Timestamp = DateTime.Now
        };
        if (ticket is null) return null!;
        ticket.Messages.Add(messageReq);
        _context.SaveChanges();
        return Task.FromResult(_mapper.Map<MessageResponse>(messageReq));
    }

    public Task<List<MessageResponse>> getTicketMessages(string ticketId)
    {
        var ticket = _context.Tickets?
            .Include(t => t.Messages)
            .First(t => t.Id == ticketId);
        var response = _mapper.Map<List<MessageResponse>>(ticket?.Messages);
        return Task.FromResult(response);
    }
}