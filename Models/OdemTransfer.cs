namespace Odem.WebAPI.Models;

public class OdemTransfer : Transaction
{
    public required Wallet From { get; init; }
    public required Wallet To { get; init; }
    public DateTime Date { get; } = DateTime.Now;
}