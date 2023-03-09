namespace Odem.WebAPI.Models;

public class Ticket
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public DateTime CreationDate { get; } = DateTime.Now;
    public Status Status { get; set; } = Status.Open;
    public DateTime CloseDate { get; set; }
    public required Admin HandledBy { get; set; }
    public required Client CreatedBy { get; init; }
}

public enum Status
{
    Open,
    Closed
}