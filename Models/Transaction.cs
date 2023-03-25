using Microsoft.EntityFrameworkCore;

namespace Odem.WebAPI.Models;
[PrimaryKey(nameof(Id))]
public abstract class Transaction
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public required double Amount { get; set; }
    public DateTime Date { get; } = DateTime.Now;
    public required TransactionType Type { get; init; }
}

public enum TransactionType
{
    Ongoing,
    Outgoing,
}