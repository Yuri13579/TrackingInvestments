using InvestmentTracking.Business.Services.Interfaces;
using InvestmentTracking.BusinessData;
using InvestmentTracking.BusinessData.Strategies;
using InvestmentTracking.Data.Repositories.Interfaces;

namespace InvestmentTracking.Business.Services
{
    public class InvestmentCalculatorFifo(ICachingPurchaseLotRepository cachingPurchaseLotRepository) : IInvestmentCalculator
    {
        public int CalculateRemainingShares(int sharesSold)
        {
            var totalShares = cachingPurchaseLotRepository.GetPurchaseLots().Sum(lot => lot.Shares);
            var remainingShares = totalShares - sharesSold;
            return remainingShares >= 0 ? remainingShares : 0;
        }

        public decimal CalculateCostBasisOfSoldShares(int accountingStrategyNumber, int sharesSold)
        {
            var strategy = GetStrategy(accountingStrategyNumber);
            return strategy.CalculateCostBasisOfSoldShares(cachingPurchaseLotRepository.GetPurchaseLots(), sharesSold);
        }

        public decimal CalculateCostBasisOfRemainingShares(int accountingStrategyNumber, int sharesSold)
        {
            var strategy = GetStrategy(accountingStrategyNumber);
            return strategy.CalculateCostBasisOfRemainingShares(cachingPurchaseLotRepository.GetPurchaseLots(),
                sharesSold);
        }

        public decimal CalculateProfit(int accountingStrategyNumber, int sharesSold, decimal salePrice)
        {
            if (sharesSold <= 0 || salePrice <= 0) return decimal.Zero;
            var strategy = GetStrategy(accountingStrategyNumber);
            var costBasis =
                strategy.CalculateCostBasisOfSoldShares(cachingPurchaseLotRepository.GetPurchaseLots(), sharesSold);

            var profit = salePrice * sharesSold - costBasis;
            return decimal.Round(profit, 2);
        }

        private AccountingStrategy GetStrategy(int accountingStrategyNumber)
        {
            return accountingStrategyNumber switch
            {
                1 => new FifoStrategy(),
                2 => new LifoStrategy(),
                3 => new AverageCostStrategy(),
                4 => new LowestTaxExposureStrategy(),
                5 => new HighestTaxExposureStrategy(),
                6 => new LotBasedStrategy(),
                _ => throw new ArgumentException("Invalid strategy number")
            };
        }
    }
}
