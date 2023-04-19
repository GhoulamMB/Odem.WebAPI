using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Odem.WebAPI.Models;
using Odem.WebAPI.Models.response;

namespace Odem.WebAPI.Services;

public class SupportService : ISupportService
{
    private readonly DataContext? _context;
    private readonly IMapper _mapper;
    public SupportService()
    {
        _context = new();
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Ticket, TicketResponse>()
                .ForMember(dest => dest.HandledBy,
                    opt => opt
                        .MapFrom(src => src.HandledBy.FirstName));
        });
        _mapper = mapperConfiguration.CreateMapper();
    }
    public Task<TicketResponse> CreateTicket(string message, string userId, string adminId)
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

    public Task<List<TicketResponse>> GetTickets()
    {
        var tickets = _context.Tickets?
            .Include(t => t.CreatedBy)
            .Include(t => t.HandledBy)
            .Include(m=>m.Messages)
            .ToList();
        var responses = new List<TicketResponse>();
        tickets?.ForEach(t=> responses.Add(_mapper.Map(t, new TicketResponse())));
        return Task.FromResult(responses);
    }
}