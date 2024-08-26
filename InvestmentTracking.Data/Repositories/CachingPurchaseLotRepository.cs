using InvestmentTracking.Data.Model;
using InvestmentTracking.Data.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace InvestmentTracking.Data.Repositories
{
    public class CachingPurchaseLotRepository : ICachingPurchaseLotRepository
    {
        private readonly IPurchaseLotRepository _repository;
        private readonly IMemoryCache _cache;

        private static readonly string CacheKey = "PurchaseLots";

        public CachingPurchaseLotRepository(IPurchaseLotRepository repository, IMemoryCache cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public List<PurchaseLot> GetPurchaseLots()
        {
            if (!_cache.TryGetValue(CacheKey, out List<PurchaseLot> purchaseLots))
            {
                // Cache data not available, fetch from repository
                purchaseLots = _repository.GetPurchaseLots();

                // Set cache options
                var cacheEntryOptions = new MemoryCacheEntryOptions();

                // Cache the data
                _cache.Set(CacheKey, purchaseLots, cacheEntryOptions);
            }

            return purchaseLots;
        }

        public PurchaseLot GetPurchaseLotById(int id)
        {
            var purchaseLots = GetPurchaseLots();
            var purchaseLot = purchaseLots.Find(lot => lot.GetHashCode() == id); // Replace with actual identifier
            return purchaseLot;
        }
    }
}
