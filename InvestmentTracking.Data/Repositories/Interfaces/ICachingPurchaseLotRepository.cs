using InvestmentTracking.Data.Model;

namespace InvestmentTracking.Data.Repositories.Interfaces;

public interface ICachingPurchaseLotRepository : IPurchaseLotRepository
{
    List<PurchaseLot> GetPurchaseLots();
    PurchaseLot GetPurchaseLotById(int id);
}