using InvestmentTracking.Data.Model;

namespace InvestmentTracking.Data.Repositories.Interfaces;

public interface IPurchaseLotRepository
{
    List<PurchaseLot> GetPurchaseLots();
}