namespace Odem.WebAPI.Models;

public abstract class Transaction
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public required Wallet From { get; init; }
    public required Wallet To { get; init; }
}