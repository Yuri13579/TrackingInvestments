using InvestmentTracking.Data.Model;

namespace InvestmentTracking.BusinessData.Strategies
{
    public class FifoStrategy() : AccountingStrategy("Fifo", 1)
    {
        public override decimal CalculateCostBasisOfSoldShares(IEnumerable<PurchaseLot> purchaseLots, int sharesSold)
        {
            if (sharesSold <= 0) return 0m;

            var currentPurchaseLots = purchaseLots.OrderBy(x => x.PurchaseDate).ToList();
            ;

            return CalculateCostOfShares(sharesSold, currentPurchaseLots);
        }

        public override decimal CalculateCostBasisOfRemainingShares(IEnumerable<PurchaseLot> purchaseLots, int sharesSold)
        {
            if (sharesSold <= 0) return 0m;

            var currentPurchaseLots = purchaseLots.OrderByDescending(x => x.PurchaseDate).ToList();

            return CalculateCostOfShares(sharesSold, currentPurchaseLots);
        }


        private decimal CalculateCostOfShares(int sharesSold, IEnumerable<PurchaseLot> purchaseLots)
        {
            var totalShares = 0;
            Queue<PurchaseLot> currentPurchaseLots = new Queue<PurchaseLot>(purchaseLots);

            currentPurchaseLots.ToList().ForEach(x => totalShares += x.Shares);

            if (sharesSold <= 0 || totalShares < sharesSold)
                throw new InvalidOperationException("Not enough shares to sell.");

            var shareSoldCurrent = sharesSold;
            decimal costBasisOfSoldShares = 0;
            foreach (var lot in currentPurchaseLots)
                if (shareSoldCurrent - lot.Shares > 0)
                {
                    shareSoldCurrent -= lot.Shares;
                    costBasisOfSoldShares = lot.Shares * lot.PricePerShare;
                }
                else
                {
                    costBasisOfSoldShares += shareSoldCurrent * lot.PricePerShare;
                    break;
                }

            return decimal.Round(costBasisOfSoldShares);
        }
    }
}
