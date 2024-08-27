using InvestmentTracking.Business.Services;
using InvestmentTracking.Business.Services.Interfaces;
using InvestmentTracking.Data.Repositories;
using InvestmentTracking.Data.Repositories.Interfaces;

namespace InvestmentTrackingApp.Server.Configurations;

public static class ServiceRegistration
{
    public static void AddProjectServices(this IServiceCollection services)
    {
        services.AddSingleton<ICachingPurchaseLotRepository, CachingPurchaseLotRepository>();
        services.AddTransient<IInvestmentCalculator, InvestmentCalculatorFifo>();

        services.AddMemoryCache();
    }
}