using Microsoft.EntityFrameworkCore;

namespace Odem.WebAPI.Models;

[PrimaryKey(nameof(Id))]
public class TransferRequest
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public required double Amount { get; init; }
    public required string From { get; set; }
    public required string To { get; set; }
    public bool Checked { get; set; }
    public required string Reason { get; set; }
    public DateTime TimeStamp { get; } = DateTime.Now;
}