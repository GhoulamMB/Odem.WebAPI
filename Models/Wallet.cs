namespace Odem.WebAPI.Models;

public class Wallet
{
    public string Id { get;} = Guid.NewGuid().ToString();
    public required Client Owner { get; set; }
    public double Balance { get; set; } = 0.0;
}