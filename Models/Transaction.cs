using Microsoft.EntityFrameworkCore;

namespace Odem.WebAPI.Models;
[PrimaryKey(nameof(Id))]
public abstract class Transaction
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public required double Amount { get; set; }
    public string? PartyOne { get; init; }
    public string? PartyTwo { get; init; }
}

public enum TransactionType
{
    Ongoing,
    Outgoing,
}