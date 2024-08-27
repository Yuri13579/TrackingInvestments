using InvestmentTracking.Business.Services.Interfaces;
using InvestmentTracking.Business.Services;
using InvestmentTracking.Data.Repositories.Interfaces;
using InvestmentTracking.Data.Repositories;

namespace InvestmentTrackingApp.Server.Configurations
{
    public static class ServiceRegistration
    {
        public static void AddProjectServices(this IServiceCollection services)
        {
            services.AddSingleton<ICachingPurchaseLotRepository, CachingPurchaseLotRepository>();
            services.AddTransient<IInvestmentCalculator, InvestmentCalculatorFifo>();

            services.AddMemoryCache();
        }
    }
}
