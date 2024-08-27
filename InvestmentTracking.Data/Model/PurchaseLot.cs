namespace InvestmentTracking.Data.Model;

public class PurchaseLot(int shares, decimal pricePerShare, DateTime purchaseDate)
{
    public int Shares { get; set; } = shares;
    public decimal PricePerShare { get; set; } = pricePerShare;
    public DateTime PurchaseDate { get; set; } = purchaseDate;

    public override string ToString()
    {
        return $"{Shares} shares purchased at {PricePerShare:C} per share on {PurchaseDate:MMMM yyyy}";
    }
}