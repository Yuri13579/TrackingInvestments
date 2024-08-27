using InvestmentTracking.Data.Model;

namespace InvestmentTracking.BusinessData.Strategies
{
    public class HighestTaxExposureStrategy() : AccountingStrategy("HighestTaxExposure", 5)
    {
        public override decimal CalculateCostBasisOfRemainingShares(IEnumerable<PurchaseLot> purchaseLots, int sharesSold)
        {
            throw new NotImplementedException();
        }

        public override decimal CalculateCostBasisOfSoldShares(IEnumerable<PurchaseLot> purchaseLots, int sharesSold)
        {
            throw new NotImplementedException();
        }
    }
}
