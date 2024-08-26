using InvestmentTracking.Data.Model;
using Microsoft.Extensions.Caching.Memory;

namespace InvestmentTrackingApp.Server.Configurations
{
    public static class CacheConfiguration
    {
        public static void PreloadCache(IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var cache = scope.ServiceProvider.GetRequiredService<IMemoryCache>();

                // Preload cache with PurchaseLots data
                var purchaseLots = new List<PurchaseLot>
                {
                    new PurchaseLot(100, 20m, new DateTime(2024, 1, 1)), 
                    new PurchaseLot(150, 30m, new DateTime(2024, 2, 1)), 
                    new PurchaseLot(120, 10m, new DateTime(2024, 3, 1)) 
                };

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                cache.Set("PurchaseLots", purchaseLots, cacheEntryOptions);
            }
        }
    }
}
