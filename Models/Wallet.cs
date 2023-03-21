using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Odem.WebAPI.Models;
[PrimaryKey(nameof(Id))]

public class Wallet
{
    public string Id { get;} = Guid.NewGuid().ToString();
    public double Balance { get; set; } = 0.0;
}