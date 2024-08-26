using InvestmentTracking.Business.Services.Interfaces;
using InvestmentTracking.Data.Model;
using InvestmentTracking.Data.Repositories.Interfaces;

namespace InvestmentTracking.Business.Services
{
    public class InvestmentCalculator : IInvestmentCalculator
    {
        private readonly ICachingPurchaseLotRepository _cachingPurchaseLotRepository;
        private readonly IEnumerable<PurchaseLot> _purchaseLots;

        public InvestmentCalculator(ICachingPurchaseLotRepository cachingPurchaseLotRepository)
        {
            _cachingPurchaseLotRepository = cachingPurchaseLotRepository;
            // Fetch the purchase lots from the cache during initialization (already ordered)
            _purchaseLots = _cachingPurchaseLotRepository.GetPurchaseLots();
        }

        public int CalculateRemainingShares(int sharesSold)
        {
            var totalShares = _purchaseLots.Sum(lot => lot.Shares);
            var remainingShares = totalShares - sharesSold;
            return remainingShares >= 0 ? remainingShares : 0;
        }

        public decimal CalculateCostBasisOfSoldShares(int sharesSold)
        {
            if (sharesSold <= 0) return 0m;

            int sharesToSell = sharesSold;
            decimal totalCost = 0m;

            foreach (var lot in _purchaseLots)
            {
                if (sharesToSell == 0) break;

                int sharesFromLot = Math.Min(sharesToSell, lot.Shares);
                totalCost += sharesFromLot * lot.PricePerShare;
                sharesToSell -= sharesFromLot;
            }

            if (sharesToSell > 0)
                throw new InvalidOperationException("Not enough shares to sell.");

            var averageCostBasis = totalCost / sharesSold;
            return decimal.Round(averageCostBasis, 2);
        }
        public decimal CalculateCostBasisOfRemainingShares(int sharesSold)
        {
            int sharesToSell = sharesSold;
            decimal totalCost = 0m;
            int totalRemainingShares = 0;

            foreach (var lot in _purchaseLots)
            {
                if (sharesToSell >= lot.Shares)
                {
                    sharesToSell -= lot.Shares;
                    continue;
                }

                int sharesRemainingInLot = lot.Shares - sharesToSell;
                totalCost += sharesRemainingInLot * lot.PricePerShare;
                totalRemainingShares += sharesRemainingInLot;
                sharesToSell = 0;
            }

            if (totalRemainingShares == 0) return 0m;

            var averageCostBasis = totalCost / totalRemainingShares;
            return decimal.Round(averageCostBasis, 2);
        }

        public decimal CalculateProfit(int sharesSold, decimal salePrice)
        {
            if (sharesSold <= 0 || salePrice <= 0) return 0m;

            var costBasis = CalculateCostBasisOfSoldShares(sharesSold);
            var profit = (salePrice - costBasis) * sharesSold;
            return decimal.Round(profit, 2);
        }
    }
}