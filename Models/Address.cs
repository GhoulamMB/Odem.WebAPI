using Microsoft.EntityFrameworkCore;

namespace Odem.WebAPI.Models;
public class Address
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string ZipCode { get; set; }
}