using Ardalis.SmartEnum;
using InvestmentTracking.Data.Model;

namespace InvestmentTracking.BusinessData;

public abstract class AccountingStrategy : SmartEnum<AccountingStrategy>
{
    public static readonly AccountingStrategy Fifo = new FifoStrategy(); // AccountingStrategy(nameof(Fifo), 1);
    //public static readonly AccountingStrategy Lifo = new AccountingStrategy(nameof(Lifo), 2);
    //public static readonly AccountingStrategy AverageCost= new AccountingStrategy(nameof(AverageCost), 3);
    //public static readonly AccountingStrategy LowestTaxExposure= new AccountingStrategy(nameof(LowestTaxExposure), 4);
    //public static readonly AccountingStrategy HighestTaxExposure= new AccountingStrategy(nameof(HighestTaxExposure), 5);
    //public static readonly AccountingStrategy LotBased= new AccountingStrategy(nameof( LotBased), 6);

    private AccountingStrategy(string name, int value) : base(name, value)
    {
    }

    public abstract decimal CalculateCostBasisOfSoldShares(List<PurchaseLot> purchaseLots, int sharesSold);
    public abstract decimal CalculateCostBasisOfRemainingShares(List<PurchaseLot> purchaseLots, int sharesSold);

    public sealed class FifoStrategy : AccountingStrategy
    {
        public FifoStrategy() : base("Fifo", 1)
        {
        }

        public override decimal CalculateCostBasisOfSoldShares(List<PurchaseLot> purchaseLots, int sharesSold)
        {
            if (sharesSold <= 0) return 0m;

            var currentPurchaseLots = purchaseLots.OrderBy(x => x.PurchaseDate).ToList();
            ;

            return CalculateCostOfShares(sharesSold, currentPurchaseLots);
        }

        public override decimal CalculateCostBasisOfRemainingShares(List<PurchaseLot> purchaseLots, int sharesSold)
        {
            if (sharesSold <= 0) return 0m;

            var currentPurchaseLots = purchaseLots.OrderByDescending(x => x.PurchaseDate).ToList();

            return CalculateCostOfShares(sharesSold, currentPurchaseLots);
        }


        private decimal CalculateCostOfShares(int sharesSold, List<PurchaseLot> currentPurchaseLots)
        {
            var totalShares = 0;
            //Queue
            currentPurchaseLots.ForEach(x => totalShares += x.Shares);

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