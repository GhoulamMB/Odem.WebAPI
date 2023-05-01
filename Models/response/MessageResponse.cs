namespace Odem.WebAPI.Models.response;

public class MessageResponse
{
    public required string Content { get; init; }
    public DateTime Timestamp { get; init; }
    public bool isClientMessage { get; init; }
}