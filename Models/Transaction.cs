using Microsoft.EntityFrameworkCore;

namespace Odem.WebAPI.Models;
[PrimaryKey(nameof(Id))]
public abstract class Transaction
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public required double Amount { get; set; }
    public string? FromName { get; init; }
    public string? ToName { get; init; }
    
    public DateTime Date { get; } = DateTime.Now;
    public required TransactionType Type { get; set; }
}

public enum TransactionType
{
    Ongoing,
    Outgoing,
}