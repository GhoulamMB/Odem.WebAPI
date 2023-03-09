namespace Odem.WebAPI.Models;

public class Admin : User
{
    public string Uid { get; } = "Admin-"+Guid.NewGuid();
}