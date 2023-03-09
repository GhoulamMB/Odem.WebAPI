using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Odem.WebAPI.Models;
[PrimaryKey(nameof(Uid))]
public class Admin : User
{
    public string Uid { get; } = "Admin-"+Guid.NewGuid();
    public List<Ticket> HandledTickets { get; set; }
}