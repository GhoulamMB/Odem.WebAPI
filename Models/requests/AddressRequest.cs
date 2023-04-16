namespace Odem.WebAPI.Models.requests;

public class AddressRequest
{
    public required string Street { get; init; }
    public required string City { get; init; }
    public required string ZipCode { get; init; }
}