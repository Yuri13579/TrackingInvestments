using InvestmentTracking.Business.Services.Interfaces;
using InvestmentTracking.BusinessData.Strategies;
using InvestmentTracking.Data.Repositories.Interfaces;


namespace InvestmentTracking.Business.Services
{
    public class InvestmentCalculator(ICachingPurchaseLotRepository cachingPurchaseLotRepository) : IInvestmentCalculator
    {
        private readonly ICachingPurchaseLotRepository _cachingPurchaseLotRepository = cachingPurchaseLotRepository;

        public int CalculateRemainingShares(int sharesSold)
        {
            var totalShares = _cachingPurchaseLotRepository.GetPurchaseLots().Sum(lot => lot.Shares);
            var remainingShares = totalShares - sharesSold;
            return remainingShares >= 0 ? remainingShares : 0;
        }

        public decimal CalculateCostBasisOfSoldShares(int sharesSold)
        {
            var strategy = new FifoStrategy();
            return strategy.CalculateCostBasisOfSoldShares(_cachingPurchaseLotRepository.GetPurchaseLots(), sharesSold);

        }

        public decimal CalculateCostBasisOfRemainingShares(int sharesSold)
        {
            var strategy = new FifoStrategy();
            return strategy.CalculateCostBasisOfRemainingShares(_cachingPurchaseLotRepository.GetPurchaseLots(), sharesSold);
        }

        public decimal CalculateProfit(int sharesSold, decimal salePrice)
        {
            if (sharesSold <= 0 || salePrice <= 0) return 0m;
            var strategy = new FifoStrategy();
            decimal costBasis = strategy.CalculateCostBasisOfSoldShares(_cachingPurchaseLotRepository.GetPurchaseLots(), sharesSold);

            //decimal costBasis = CalculateCostBasisOfSoldShares(sharesSold);
            decimal profit = (salePrice * sharesSold) - costBasis;
            return decimal.Round(profit, 2);
        }
    }
}