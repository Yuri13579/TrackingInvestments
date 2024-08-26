using InvestmentTracking.Business.Services.Interfaces;
using InvestmentTracking.Data.Model;
using InvestmentTracking.Data.Repositories.Interfaces;
using System.Collections.Generic;

namespace InvestmentTracking.Business.Services
{
    public class InvestmentCalculator : IInvestmentCalculator
    {
        private readonly ICachingPurchaseLotRepository _cachingPurchaseLotRepository;
        private readonly IEnumerable<PurchaseLot> _purchaseLots;

        public InvestmentCalculator(ICachingPurchaseLotRepository cachingPurchaseLotRepository)
        {
            _cachingPurchaseLotRepository = cachingPurchaseLotRepository;
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

            List<PurchaseLot> currentPurchaseLots = _purchaseLots.ToList();

            return CalculateCostOfShares(sharesSold, currentPurchaseLots);
        }

        public decimal CalculateCostBasisOfRemainingShares(int sharesSold)
        {
            if (sharesSold <= 0) return 0m;

            List<PurchaseLot> currentPurchaseLots = _purchaseLots.ToList().OrderByDescending(x => x.PurchaseDate).ToList();

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
            foreach (var lot in _purchaseLots)
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