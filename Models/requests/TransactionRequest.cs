namespace Odem.WebAPI.Models.requests;

public class TransactionRequest
{
    public required double Amount { get; init; }
    public required string FromWalletId { get; init; }
    public required string ToWalletId { get; init; }
}