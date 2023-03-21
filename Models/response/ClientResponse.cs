﻿namespace Odem.WebAPI.Models.response;

public class ClientResponse
{
    public string Uid { get; init; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string Password { get; set; }
    public required Address Address { get; set; }
    public Wallet Wallet { get; set; }
    public List<TicketResponse> Tickets { get; set; }
}