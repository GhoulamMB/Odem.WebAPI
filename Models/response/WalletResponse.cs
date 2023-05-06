namespace Odem.WebAPI.Models.response;

public class WalletResponse
{
    public string? Id { get; init; }
    public double Balance { get; init; }
    public List<OdemTransferResponse>? Transactions { get; init; }
}