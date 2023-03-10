using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Odem.WebAPI.Models;
[PrimaryKey(nameof(Uid))]
public class Client:User
{
    public string Uid { get; } = Guid.NewGuid().ToString();
    
    public ClientStatus Status { get; set; } = ClientStatus.Active;
    public required Wallet Wallet { get; set; }

    public List<Ticket> Tickets { get; set; } = new();
}

public enum ClientStatus
{
    Active,
    Disabled
}