namespace InvestmentTracking.Business.Services.Interfaces
{
    public interface IInvestmentCalculator
    {
        int CalculateRemainingShares(int sharesSold);
        decimal CalculateCostBasisOfSoldShares(int sharesSold);
        decimal CalculateCostBasisOfRemainingShares(int sharesSold);
        decimal CalculateProfit(int sharesSold, decimal salePrice);
    }
}
