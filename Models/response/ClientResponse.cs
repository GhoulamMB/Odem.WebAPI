namespace Odem.WebAPI.Models.response;

public class ClientResponse
{
    public string? Uid { get; init; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string Password { get; set; }
    public required Address Address { get; set; }
    public WalletResponse? Wallet { get; set; }
    public List<TicketResponse>? Tickets { get; set; }
}

public class WalletResponse
{ 
    public string? Id { get; init; }
    public double Balance { get; set; }
    public List<TransactionResponse>? Transactions { get; init; }
}

public class TransactionResponse
{
    public string? Id { get; init; }
    public double Amount { get; init; }
    public DateTime Date { get; init; }
    public required TransactionType Type { get; init; }
    public required string From { get; init; }
}