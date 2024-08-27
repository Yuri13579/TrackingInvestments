using Ardalis.SmartEnum;
using InvestmentTracking.BusinessData.Strategies;
using InvestmentTracking.Data.Model;

namespace InvestmentTracking.BusinessData;

public abstract class AccountingStrategy(string name, int value) : SmartEnum<AccountingStrategy>(name, value)
{
    public static readonly AccountingStrategy Fifo = new FifoStrategy();
    public static readonly AccountingStrategy Lifo = new LifoStrategy();
    public static readonly AccountingStrategy AverageCost = new AverageCostStrategy();
    public static readonly AccountingStrategy LowestTaxExposure = new LowestTaxExposureStrategy();
    public static readonly AccountingStrategy HighestTaxExposure = new HighestTaxExposureStrategy();
    public static readonly AccountingStrategy LotBased = new LotBasedStrategy();

    public abstract decimal CalculateCostBasisOfSoldShares(IEnumerable<PurchaseLot> purchaseLots, int sharesSold);
    public abstract decimal CalculateCostBasisOfRemainingShares(IEnumerable<PurchaseLot> purchaseLots, int sharesSold);
}