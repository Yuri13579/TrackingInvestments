using InvestmentTracking.Data.Model;
using InvestmentTracking.Data.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace InvestmentTracking.Data.Repositories
{
    public class CachingPurchaseLotRepository : ICachingPurchaseLotRepository
    {
        private readonly IMemoryCache _cache;
        private static readonly string CacheKey = "PurchaseLots";

        public CachingPurchaseLotRepository(IMemoryCache cache)
        {
            _cache = cache;
        }

        public List<PurchaseLot> GetPurchaseLots()
        {
            // Retrieve the cached value directly
            if (_cache.TryGetValue(CacheKey, out List<PurchaseLot> purchaseLots))
            {
                // Return the collection ordered by PricePerShare
                return purchaseLots.OrderBy(lot => lot.PricePerShare).ToList();
            }

            // Return an empty list or handle the case when data is not in the cache
            return new List<PurchaseLot>();
        }

        public PurchaseLot GetPurchaseLotById(int id)
        {
            var purchaseLots = GetPurchaseLots();
            var purchaseLot = purchaseLots.Find(lot => lot.GetHashCode() == id); // Replace with actual identifier
            return purchaseLot;
        }
    }
}