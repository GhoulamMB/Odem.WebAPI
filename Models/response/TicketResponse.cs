namespace Odem.WebAPI.Models.response;

public class TicketResponse
{
    public string? Id { get; init; }
    public DateTime CreationDate { get; init; }
    public Status Status { get; init; }
    public DateTime CloseDate { get; init; }
    public string? HandledBy { get; init; }
    public List<MessageResponse>? Messages { get; set; }
}