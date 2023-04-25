namespace Odem.WebAPI.Models.response;

public class OdemTransferResponse
{
    public string From { get; set; }
    public string To { get; set; }
    public double Amount { get; set; }
    public DateTime Date { get; set; }
    public TransactionType Type { get; set; }
}