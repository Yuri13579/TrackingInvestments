using InvestmentTracking.Data.Model;

namespace InvestmentTracking.Data.Repositories.Interfaces;

public interface ICachingPurchaseLotRepository
{
    List<PurchaseLot> GetPurchaseLots();
    PurchaseLot GetPurchaseLotById(int id);
}