using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Odem.WebAPI.Models;
[PrimaryKey(nameof(Uid))]
public class Client:User
{
    public string Uid { get; } = Guid.NewGuid().ToString();
    public required Wallet Wallet { get; set; }

    public List<Ticket> Tickets { get; set; }
}