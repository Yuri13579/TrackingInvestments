using InvestmentTracking.Business.Services.Interfaces;
using InvestmentTracking.Data.Model;
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
            if (sharesSold <= 0) return 0m;

            List<PurchaseLot> currentPurchaseLots = _cachingPurchaseLotRepository.GetPurchaseLots().OrderBy(x => x.PurchaseDate).ToList();;

            return CalculateCostOfShares(sharesSold, currentPurchaseLots);
        }

        public decimal CalculateCostBasisOfRemainingShares(int sharesSold)
        {
            if (sharesSold <= 0) return 0m;

            List<PurchaseLot> currentPurchaseLots = _cachingPurchaseLotRepository.GetPurchaseLots().OrderByDescending(x => x.PurchaseDate).ToList();

            return CalculateCostOfShares(sharesSold, currentPurchaseLots);
        }

        public decimal CalculateProfit(int sharesSold, decimal salePrice)
        {
            if (sharesSold <= 0 || salePrice <= 0) return 0m;

            decimal costBasis = CalculateCostBasisOfSoldShares(sharesSold);
            decimal profit = (salePrice * sharesSold) - costBasis;
            return decimal.Round(profit, 2);
        }
        private decimal CalculateCostOfShares(int sharesSold, List<PurchaseLot> currentPurchaseLots)
        {
            int totalShares = 0;

            currentPurchaseLots.ForEach(x => totalShares += x.Shares);

            if (sharesSold <= 0 || totalShares < sharesSold)
                throw new InvalidOperationException("Not enough shares to sell.");

            int shareSoldCurrent = sharesSold;
            decimal costBasisOfSoldShares = 0;
            foreach (var lot in currentPurchaseLots)
            {
                if (shareSoldCurrent - lot.Shares > 0)
                {
                    shareSoldCurrent -= lot.Shares;
                    costBasisOfSoldShares = lot.Shares * lot.PricePerShare;
                    continue;
                }
                else
                {
                    costBasisOfSoldShares += shareSoldCurrent * lot.PricePerShare;
                    break;
                }

            }
            return decimal.Round(costBasisOfSoldShares);
        }

    }
}