namespace Odem.WebAPI.Models.response;

public class OdemTransferResponse
{
    public required string PartyOne { get; set; }
    public required string PartyTwo { get; set; }
    public double Amount { get; set; }
    public DateTime Date { get; set; }
}