namespace Odem.WebAPI.Models.response;

public class OdemTransferResponse
{
    public string FromName { get; set; }
    public string ToName { get; set; }
    public double Amount { get; set; }
    public DateTime Date { get; set; }
    public TransactionType Type { get; set; }
}