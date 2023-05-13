namespace Odem.WebAPI.Models;

public class OdemTransfer : Transaction
{
    public required Wallet From { get; init; }
    public required Wallet To { get; init; }
    public required DateTime Date { get; init; }
}