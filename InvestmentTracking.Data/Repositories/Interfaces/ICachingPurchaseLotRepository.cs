using InvestmentTracking.Data.Model;

namespace InvestmentTracking.Data.Repositories.Interfaces;

public interface ICachingPurchaseLotRepository
{
    IEnumerable<PurchaseLot> GetPurchaseLots();
    PurchaseLot? GetPurchaseLotById(int id);
}