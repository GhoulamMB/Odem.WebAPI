﻿using Odem.WebAPI.Models.response;

namespace Odem.WebAPI.Services;

public interface ISupportService
{
    public Task<TicketResponse> CreateTicket(string message, string userId);
    public Task<TicketResponse> GetTicket(string ticketId);
    public Task<List<TicketResponse>> GetTickets(string userId);
    public Task<MessageResponse> UpdateTicket(string ticketId,string message);
    public Task<List<MessageResponse>> getTicketMessages(string ticketId);
}