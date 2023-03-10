using Microsoft.EntityFrameworkCore;

namespace Odem.WebAPI.Models;
[PrimaryKey(nameof(Id))]
public abstract class Transaction
{
    public string Id { get; } = Guid.NewGuid().ToString();
}