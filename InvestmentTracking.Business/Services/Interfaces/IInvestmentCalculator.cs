namespace InvestmentTracking.Business.Services.Interfaces;

public interface IInvestmentCalculator
{
    int CalculateRemainingShares(int sharesSold);
    decimal CalculateCostBasisOfSoldShares(int accountingStrategyNumber, int sharesSold);
    decimal CalculateCostBasisOfRemainingShares(int accountingStrategyNumber, int sharesSold);
    decimal CalculateProfit(int accountingStrategyNumber, int sharesSold, decimal salePrice);
}