namespace InvestmentTracking.Data.Model
{
    public class PurchaseLot(int shares, decimal pricePerShare)
    {
        public int Shares { get; set; } = shares;
        public decimal PricePerShare { get; set; } = pricePerShare;
    }
}
