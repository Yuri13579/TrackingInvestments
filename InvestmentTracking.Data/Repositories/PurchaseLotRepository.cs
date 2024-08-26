using InvestmentTracking.Data.Model;
using InvestmentTracking.Data.Repositories.Interfaces;

namespace InvestmentTracking.Data.Repositories
{
    public class PurchaseLotRepository : IPurchaseLotRepository
    {
        public List<PurchaseLot> GetPurchaseLots()
        {
            return new List<PurchaseLot>
            {
                new PurchaseLot(100, 20m),
                new PurchaseLot(150, 30m),
                new PurchaseLot(120, 10m)
            };
        }
    }
}
