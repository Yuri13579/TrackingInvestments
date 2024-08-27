using InvestmentTracking.Data.Model;
using InvestmentTracking.Data.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace InvestmentTracking.Data.Repositories;

public class CachingPurchaseLotRepository(IMemoryCache cache) : ICachingPurchaseLotRepository
{
    private static readonly string CacheKey = "PurchaseLots";

    public IEnumerable<PurchaseLot> GetPurchaseLots()
    {
        // Retrieve the cached value directly
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        if (cache.TryGetValue(CacheKey, out IEnumerable<PurchaseLot> purchaseLots))
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            // Return the collection ordered by PricePerShare
            if (purchaseLots != null)
                return purchaseLots;

        // Return an empty list or handle the case when data is not in the cache
        return [];
    }

    public PurchaseLot? GetPurchaseLotById(int id)
    {
        var purchaseLots = GetPurchaseLots();
        var purchaseLot = purchaseLots.ToList().Find(lot => lot.GetHashCode() == id); // Replace with actual identifier
        return purchaseLot ?? null;
    }
}