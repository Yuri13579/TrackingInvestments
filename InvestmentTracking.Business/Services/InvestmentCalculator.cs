using InvestmentTracking.Business.Services.Interfaces;
using InvestmentTracking.Data.Model;
using InvestmentTracking.Data.Repositories;
using InvestmentTracking.Data.Repositories.Interfaces;

namespace InvestmentTracking.Business.Services
{
    public class InvestmentCalculator() : IInvestmentCalculator
    {
        private readonly IPurchaseLotRepository _repository;
        private readonly List<PurchaseLot> _purchaseLots;

        public InvestmentCalculator(IPurchaseLotRepository repository) : this()
        {
            _repository = repository;
            _purchaseLots = _repository.GetPurchaseLots();
        }

        public int CalculateRemainingShares(int sharesSold)
        {
            int totalShares = 0;
            foreach (var lot in _purchaseLots)
            {
                totalShares += lot.Shares;
            }

            return totalShares - sharesSold;
        }

        public decimal CalculateCostBasisOfSoldShares(int sharesSold)
        {
            int remainingSharesToSell = sharesSold;
            decimal totalCost = 0;

            foreach (var lot in _purchaseLots)
            {
                if (remainingSharesToSell <= 0) break;

                int sharesToSellFromLot = Math.Min(remainingSharesToSell, lot.Shares);
                totalCost += sharesToSellFromLot * lot.PricePerShare;
                remainingSharesToSell -= sharesToSellFromLot;
            }

            return totalCost / sharesSold;
        }

        public decimal CalculateCostBasisOfRemainingShares(int sharesSold)
        {
            int remainingSharesToSell = sharesSold;
            decimal totalCost = 0;
            int totalRemainingShares = 0;

            foreach (var lot in _purchaseLots)
            {
                if (remainingSharesToSell > 0)
                {
                    int sharesToSellFromLot = Math.Min(remainingSharesToSell, lot.Shares);
                    remainingSharesToSell -= sharesToSellFromLot;
                }

                if (remainingSharesToSell <= 0)
                {
                    int sharesRemainingInLot = lot.Shares - (sharesSold - remainingSharesToSell);
                    totalCost += sharesRemainingInLot * lot.PricePerShare;
                    totalRemainingShares += sharesRemainingInLot;
                }
            }

            return totalRemainingShares > 0 ? totalCost / totalRemainingShares : 0;
        }

        public decimal CalculateProfit(int sharesSold, decimal salePrice)
        {
            decimal costBasis = CalculateCostBasisOfSoldShares(sharesSold);
            return sharesSold * (salePrice - costBasis);
        }
    }
}
