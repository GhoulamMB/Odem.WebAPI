namespace Odem.WebAPI.Models;

public class Client:User
{
    public string Uid { get; } = Guid.NewGuid().ToString();
    public required Wallet Wallet { get; set; }
}