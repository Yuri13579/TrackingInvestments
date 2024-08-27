using InvestmentTracking.Data.Model;

namespace InvestmentTracking.BusinessData.Strategies
{
    public class LowestTaxExposureStrategy() : AccountingStrategy("LowestTaxExposure", 4)
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
