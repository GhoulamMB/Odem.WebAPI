using Microsoft.EntityFrameworkCore;

namespace Odem.WebAPI.Models;
[PrimaryKey(nameof(Uid))]
public class Client:User
{
    public string Uid { get; } = Guid.NewGuid().ToString();
    
    public ClientStatus Status { get; set; } = ClientStatus.Active;
    public Wallet Wallet { get; set; } = new();

    public List<Ticket> Tickets { get; set; } = new();
}

public enum ClientStatus
{
    Active=1,
    Disabled=0
}