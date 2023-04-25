namespace Odem.WebAPI.Models.requests;

public class TransactionRequest
{
    public required double Amount { get; init; }
    public required string FromEmail { get; init; }
    public required string ToEmail { get; init; }
}