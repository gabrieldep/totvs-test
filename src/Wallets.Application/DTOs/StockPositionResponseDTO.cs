namespace Wallets.Application.DTOs;

public class StockPositionResponseDTO
{
    public string Code { get; set; }
    public int Amount { get; set; }
    public decimal ValuePerQuota { get; set; }
    public decimal Total => Amount * ValuePerQuota;
}