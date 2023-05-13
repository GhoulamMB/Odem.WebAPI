using Microsoft.EntityFrameworkCore;

namespace Odem.WebAPI.Models;

[PrimaryKey(nameof(Uid))]
public class OneSignalIds
{
    public required string Uid { get; set; }
    public required string PlayerId { get; set; }
}